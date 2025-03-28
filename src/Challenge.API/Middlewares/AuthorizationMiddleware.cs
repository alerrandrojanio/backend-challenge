using Challenge.API.Resources;
using Challenge.Domain.Entities;
using Challenge.Domain.Exceptions;
using Challenge.Domain.Interfaces;
using Challenge.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Challenge.API.Middlewares;

public class AuthorizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly AuthSettings _authSettings;

    public AuthorizationMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory, IOptions<AuthSettings> authSettings)
    {
        _next = next;
        _scopeFactory = scopeFactory;
        _authSettings = authSettings.Value;
    }

    public async Task Invoke(HttpContext context)
    {
        string path = context.Request.Path.Value?.ToLower()!;

        HashSet<string> publicRoutes = _authSettings.PublicRoutes.Split(';', StringSplitOptions.RemoveEmptyEntries).ToHashSet();

        if (publicRoutes.Contains(path))
        {
            await _next(context);
            
            return;
        }

        string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrEmpty(token))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            
            await context.Response.WriteAsync(ResourceMsg.Authorization_Token_Missing);
            
            return;
        }

        try
        {
            JwtSecurityTokenHandler tokenHandler = new();
            
            byte[] key = Encoding.UTF8.GetBytes(_authSettings.SecretKey);

            TokenValidationParameters validationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

            JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;

            if (jwtToken.ValidTo < DateTime.UtcNow)
                throw new ServiceException(ResourceMsg.Authorization_Token_Expired, HttpStatusCode.Unauthorized);

            string? userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId is null)
                throw new ServiceException(ResourceMsg.Authorization_Token_Invalid, HttpStatusCode.Unauthorized);

            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
                IUserRepository scopedUserRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

                User? user = scopedUserRepository.GetUserById(Guid.Parse(userId));

                if (user is null)
                    throw new ServiceException(ResourceMsg.Authorization_Token_Invalid, HttpStatusCode.Unauthorized);
            }
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            
            if (ex is SecurityTokenExpiredException)
                await context.Response.WriteAsync(ResourceMsg.Authorization_Token_Expired);
            else
                await context.Response.WriteAsync(ex is ServiceException ? ex.Message : ResourceMsg.Authorization_Token_Invalid);

            return;
        }
        
        await _next(context);
    }
}
