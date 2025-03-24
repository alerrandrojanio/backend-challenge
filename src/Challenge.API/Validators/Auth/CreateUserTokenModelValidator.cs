using Challenge.API.Models.Auth;
using Challenge.API.Resources;
using FluentValidation;

namespace Challenge.API.Validators.Auth;

public class CreateUserTokenModelValidator : AbstractValidator<CreateUserTokenModel>
{
    public CreateUserTokenModelValidator()
    {
        #region UserId
        RuleFor(userToken => userToken.UserId)
            .NotEmpty()
            .WithMessage(userToken => string.Format(ResourceMsg.Property_Empty, nameof(userToken.UserId)));

        RuleFor(userToken => userToken.UserId)
           .Matches(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$")
           .When(userToken => userToken.UserId is not null)
           .WithMessage(userToken => string.Format(ResourceMsg.Property_Invalid_Format, nameof(userToken.UserId)));
        #endregion UserId

        #region Password
        RuleFor(userToken => userToken.Password)
            .NotEmpty()
            .WithMessage(userToken => string.Format(ResourceMsg.Property_Empty, nameof(userToken.Password)));

        RuleFor(userToken => userToken.Password)
            .MinimumLength(8)
            .WithMessage(user => string.Format(ResourceMsg.Property_MinimumLength, nameof(user.Password), 8));
        #endregion Password
    }
}
