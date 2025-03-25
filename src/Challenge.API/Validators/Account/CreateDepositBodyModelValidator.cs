using Challenge.API.Models.Account;
using Challenge.API.Resources;
using FluentValidation;

namespace Challenge.API.Validators.Account;

public class CreateDepositBodyModelValidator : AbstractValidator<CreateDepositBodyModel>
{
    public CreateDepositBodyModelValidator()
    {
        #region PersonId
        RuleFor(deposit => deposit.PersonId)
            .NotEmpty()
            .WithMessage(deposit => string.Format(ResourceMsg.Property_Empty, nameof(deposit.PersonId)));

        RuleFor(deposit => deposit.PersonId)
           .Matches(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$")
           .When(deposit => deposit.PersonId is not null)
           .WithMessage(deposit => string.Format(ResourceMsg.Property_Invalid_Format, nameof(deposit.PersonId)));
        #endregion PersonId

        #region Value
        RuleFor(deposit => deposit.Value)
            .NotEmpty()
            .WithMessage(deposit => string.Format(ResourceMsg.Property_Empty, nameof(deposit.Value)));

        RuleFor(deposit => deposit.Value)
            .GreaterThan(0)
            .WithMessage(deposit => string.Format(ResourceMsg.Property_GreaterThan, nameof(deposit.Value), 0));
        #endregion Value
    }
}
