using Challenge.API.Models.Person;
using FluentValidation;
using Challenge.API.Resources;

namespace Challenge.API.Validators.Person;

public class CreateIndividualPersonModelValidator : AbstractValidator<CreateIndividualPersonModel>
{
    public CreateIndividualPersonModelValidator()
    {
        #region Name
        RuleFor(person => person.Name)
            .NotEmpty()
            .WithMessage(person => string.Format(ResourceMsg.Property_Empty, nameof(person.Name)));

        RuleFor(person => person.Name)
            .MinimumLength(3)
            .WithMessage(person => string.Format(ResourceMsg.Property_MinimumLength, nameof(person.Name), 3));
        #endregion Name

        #region Email
        RuleFor(person => person.Email)
            .NotEmpty()
            .WithMessage(person => string.Format(ResourceMsg.Property_Empty, nameof(person.Email)));

        RuleFor(person => person.Email)
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
            .When(person => person.Email is not null)
            .WithMessage(ResourceMsg.Property_Email_Format);
        #endregion Email

        #region Phone
        RuleFor(person => person.Phone)
            .NotEmpty()
            .WithMessage(person => string.Format(ResourceMsg.Property_Empty, nameof(person.Phone)));

        RuleFor(person => person.Phone)
            .Matches(@"^(\(?\d{2}\)?\s?)?(\d{4,5})[-.\s]?\d{4}$")
            .When(person => person.Phone is not null)
            .WithMessage(ResourceMsg.Property_Phone_Format);
        #endregion Phone

        #region CPF
        RuleFor(person => person.CPF)
            .NotEmpty()
            .WithMessage(person => string.Format(ResourceMsg.Property_Empty, nameof(person.CPF)));

        RuleFor(person => person.CPF)
            .Length(11)
            .When(person => person.CPF is not null)
            .WithMessage(person => string.Format(ResourceMsg.Property_Length, nameof(person.CPF), 11));

        RuleFor(person => person.CPF)
            .Matches(@"^\d+$")
            .When(person => person.CPF is not null)
            .WithMessage(person => string.Format(ResourceMsg.Property_OnlyDigits, nameof(person.CPF)));
        #endregion CPF

        #region BirthDate
        RuleFor(person => person.BirthDate)
            .NotEmpty()
            .WithMessage(person => string.Format(ResourceMsg.Property_Empty, nameof(person.BirthDate)));

        RuleFor(person => person.BirthDate)
            .Must(birthDate => DateTime.TryParse(birthDate, out _))
            .When(person => person.BirthDate is not null)
            .WithMessage(ResourceMsg.Property_BirthDate_Invalid);

        RuleFor(person => person.BirthDate)
            .Must(birthDate => DateTime.TryParse(birthDate, out var date) && date <= DateTime.Today)
            .When(person => person.BirthDate is not null && DateTime.TryParse(person.BirthDate, out _))
            .WithMessage(ResourceMsg.Property_BirthDate_Future);
        #endregion BirthDate
    }
}
