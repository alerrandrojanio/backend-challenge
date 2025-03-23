using Challenge.API.Models.Account;
using Challenge.API.Resources;
using FluentValidation;

namespace Challenge.API.Validators.Account;

public class CreateTransferModelValidator : AbstractValidator<CreateTransferModel>
{
    public CreateTransferModelValidator()
    {
        #region PayerId
        RuleFor(account => account.PayerId)
            .NotEmpty()
            .NotNull()
            .WithMessage(account => string.Format(ResourceMsg.Property_Empty, nameof(account.PayerId)));

        RuleFor(account => account.PayerId)
           .Must(payerId => Guid.TryParse(payerId, out _))
           .When(account => account.PayerId is not null)
           .WithMessage(account => string.Format(ResourceMsg.Property_Invalid_Format, nameof(account.PayerId)));
        #endregion PayerId

        #region PayeeId
        RuleFor(account => account.PayeeId)
            .NotEmpty()
            .NotNull()
            .WithMessage(account => string.Format(ResourceMsg.Property_Empty, nameof(account.PayeeId)));

        RuleFor(account => account.PayerId)
           .Must(payeeId => Guid.TryParse(payeeId, out _))
           .When(account => account.PayeeId is not null)
           .WithMessage(account => string.Format(ResourceMsg.Property_Invalid_Format, nameof(account.PayeeId)));
        #endregion PayeeId

        #region Value
        RuleFor(account => account.Value)
            .NotEmpty()
            .NotNull()
            .WithMessage(account => string.Format(ResourceMsg.Property_Empty, nameof(account.Value)));

        RuleFor(account => account.Value)
            .GreaterThan(0)
            .WithMessage(account => string.Format(ResourceMsg.Property_GreaterThan, nameof(account.Value), 0));
        #endregion Value
    }
}
