﻿using Challenge.API.Models.Person;
using Challenge.API.Resources;
using FluentValidation;

namespace Challenge.API.Validators.Person;

public class CreateMerchantPersonModelValidator : AbstractValidator<CreateMerchantPersonModel>
{
    public CreateMerchantPersonModelValidator()
    {
        #region Name
        RuleFor(person => person.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage(person => string.Format(ResourceMsg.Property_Empty, nameof(person.Name)));

        RuleFor(person => person.Name)
            .MinimumLength(3)
            .WithMessage(person => string.Format(ResourceMsg.Property_MinimumLength, nameof(person.Name), 3));
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

        #region Phone
        RuleFor(person => person.Phone)
            .NotEmpty()
            .NotNull()
            .WithMessage(person => string.Format(ResourceMsg.Property_Empty, nameof(person.Phone)));

        RuleFor(person => person.Phone)
            .Matches(@"^(\(?\d{2}\)?\s?)?(\d{4,5})[-.\s]?\d{4}$")
            .When(person => person.Phone is not null)
            .WithMessage(ResourceMsg.Property_Phone_Format);
        #endregion Phone

        #region CPF
        RuleFor(person => person.CNPJ)
            .NotEmpty()
            .NotNull()
            .WithMessage(person => string.Format(ResourceMsg.Property_Empty, nameof(person.CNPJ)));

        RuleFor(person => person.CNPJ)
            .Length(14)
            .WithMessage(person => string.Format(ResourceMsg.Property_Length, nameof(person.CNPJ), 14));
        #endregion CPF

        #region MerchantName
        #endregion MerchantName

        #region MerchantAddress
        #endregion MerchantAddress

        #region MerchantContact
        #endregion MerchantContact
    }
}
