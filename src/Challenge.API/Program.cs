using Challenge.API.Middlewares;
using Challenge.API.Validators.Account;
using Challenge.DI.PipelineExtensions;
using Challenge.Infrastructure.Mapping;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddServices();
builder.Services.ConfigureSettings(builder.Configuration);

MappingConfig.RegisterMappings();

ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;


builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddFluentValidators(Assembly.GetExecutingAssembly());

builder.Services.AddRedisConnection(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddApiVersioning(setup =>
{
    setup.DefaultApiVersion = ApiVersion.Default; 
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.ReportApiVersions = true;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<AuthorizationMiddleware>();

app.UseMiddleware<GlobalErrorHandlerMiddleware>();

app.Run();
