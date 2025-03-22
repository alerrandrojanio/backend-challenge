using Challenge.API.Models.User;
using Challenge.API.Resources;
using FluentValidation;

namespace Challenge.API.Validators.User;

public class CreateUserModelValidator : AbstractValidator<CreateUserModel>
{
    public CreateUserModelValidator()
    {
        #region Name
        RuleFor(user => user.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage(user => string.Format(ResourceMsg.Property_Empty, nameof(user.Name)));

        RuleFor(user => user.Name)
            .MinimumLength(3)
            .WithMessage(user => string.Format(ResourceMsg.Property_MinimumLength, nameof(user.Name), 3));
        #endregion Name

        #region Email
        RuleFor(person => person.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage(person => string.Format(ResourceMsg.Property_Empty, nameof(person.Email)));

        RuleFor(person => person.Email)
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
            .When(person => person.Email is not null)
            .WithMessage(ResourceMsg.Property_Email_Format);
        #endregion Email

        #region Password
        RuleFor(user => user.Password)
           .NotEmpty()
           .NotNull()
           .WithMessage(user => string.Format(ResourceMsg.Property_Empty, nameof(user.Password)));

        RuleFor(user => user.Password)
            .MinimumLength(8)
            .WithMessage(user => string.Format(ResourceMsg.Property_MinimumLength, nameof(user.Password), 8));
        #endregion Password
    }
}
