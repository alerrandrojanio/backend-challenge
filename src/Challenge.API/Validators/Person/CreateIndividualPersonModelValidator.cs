using Challenge.API.Models.Person;
using FluentValidation;
using Challenge.API.Resources;

namespace Challenge.API.Validators.Person;

public class CreateIndividualPersonModelValidator : AbstractValidator<CreateIndividualPersonModel>
{
    public CreateIndividualPersonModelValidator()
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
        RuleFor(user => user.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage(user => string.Format(ResourceMsg.Property_Empty, nameof(user.Email)));

        RuleFor(user => user.Email)
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
            .When(user => user.Email is not null)
            .WithMessage(ResourceMsg.Property_Email_Format);
        #endregion Email

        #region Phone
        RuleFor(user => user.Phone)
            .NotEmpty()
            .NotNull()
            .WithMessage(user => string.Format(ResourceMsg.Property_Empty, nameof(user.Phone)));

        RuleFor(user => user.Phone)
            .Matches(@"^(\(?\d{2}\)?\s?)?(\d{4,5})[-.\s]?\d{4}$")
            .When(user => user.Phone is not null)
            .WithMessage(ResourceMsg.Property_Phone_Format);
        #endregion Phone

        #region CPF
        RuleFor(user => user.CPF)
            .NotEmpty()
            .NotNull()
            .WithMessage(user => string.Format(ResourceMsg.Property_Empty, nameof(user.CPF)));

        RuleFor(user => user.CPF)
            .Length(11)
            .WithMessage(user => string.Format(ResourceMsg.Property_Length, nameof(user.CPF), 11));
        #endregion CPF

        #region BirthDate
        RuleFor(user => user.BirthDate)
            .NotEmpty()
            .NotNull()
            .WithMessage(user => string.Format(ResourceMsg.Property_Empty, nameof(user.BirthDate)));
        #endregion BirthDate
    }
}
