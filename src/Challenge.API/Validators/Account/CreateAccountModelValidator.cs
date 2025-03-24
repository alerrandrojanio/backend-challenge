using Challenge.API.Models.Account;
using Challenge.API.Resources;
using FluentValidation;

namespace Challenge.API.Validators.Account;

public class CreateAccountModelValidator : AbstractValidator<CreateAccountModel>
{
    public CreateAccountModelValidator()
    {
        #region PersonId
        RuleFor(account => account.PersonId)
            .NotEmpty()
            .WithMessage(account => string.Format(ResourceMsg.Property_Empty, nameof(account.PersonId)));

        RuleFor(account => account.PersonId)
           .Matches(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$")
           .When(account => account.PersonId is not null)
           .WithMessage(account => string.Format(ResourceMsg.Property_Invalid_Format, nameof(account.PersonId)));
        #endregion PersonId

        #region AccountNumber
        RuleFor(account => account.AccountNumber)
            .NotEmpty()
            .WithMessage(account => string.Format(ResourceMsg.Property_Empty, nameof(account.AccountNumber)));

        RuleFor(account => account.AccountNumber)
            .Matches(@"^\d{6}$")
            .When(account => account.AccountNumber is not null)
            .WithMessage(account => string.Format(ResourceMsg.Property_Invalid_Format, nameof(account.AccountNumber)));
        #endregion AccountNumber

        #region Balance
        RuleFor(account => account.Balance)
            .NotEmpty()
            .WithMessage(account => string.Format(ResourceMsg.Property_Empty, nameof(account.Balance)));

        RuleFor(account => account.Balance)
            .GreaterThanOrEqualTo(0)
            .WithMessage(account => string.Format(ResourceMsg.Property_GreaterThanOrEqualTo, nameof(account.Balance), 0));
        #endregion Balance
    }
}
