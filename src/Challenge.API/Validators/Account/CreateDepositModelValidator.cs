using Challenge.API.Models.Account;
using Challenge.API.Resources;
using FluentValidation;

namespace Challenge.API.Validators.Account;

public class CreateDepositModelValidator : AbstractValidator<CreateDepositModel>
{
    public CreateDepositModelValidator()
    {
        #region AccountNumber
        RuleFor(deposit => deposit.AccountNumber)
            .NotEmpty()
            .WithMessage(deposit => string.Format(ResourceMsg.Property_Empty, nameof(deposit.AccountNumber)));

        RuleFor(deposit => deposit.AccountNumber)
            .Matches(@"^\d{6}$")
            .When(deposit => deposit.AccountNumber is not null)
            .WithMessage(deposit => string.Format(ResourceMsg.Property_Invalid_Format, nameof(deposit.AccountNumber)));
        #endregion AccountNumber

        #region Body
        RuleFor(deposit => deposit.Body)
            .SetValidator(new CreateDepositBodyModelValidator()!);
        #endregion Body
    }
}
