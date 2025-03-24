using Challenge.API.Models.Account;
using Challenge.API.Validators.Account;
using FluentAssertions;
using FluentValidation.Results;

namespace Challenge.API.Tests.Validators.Account;

[TestFixture]
public class CreateAccountModelValidatorTests
{
    private CreateAccountModel _model;
    private CreateAccountModelValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _model = new()
        {
            PersonId = Guid.NewGuid().ToString(),
            AccountNumber = "123456",
            Balance = 1000
        };

        _validator = new();
    }

    [TearDown]
    public void TearDown()
    {
        _model = default!;
        _validator = default!;
    }

    [Test]
    public void CreateAccount_WhenModelIsValid_ThenNotReturnsErrors()
    {
        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    #region PersonId
    [Test]
    public void CreateAccount_WhenPersonIdIsNull_ThenReturnsAError()
    {
        // Arrange
        string errorMessage = "A propriedade PersonId é obrigatória";
        _model.PersonId = null!;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == nameof(_model.PersonId) && x.ErrorMessage == errorMessage);
    }

    [Test]
    [TestCase("", Description = "PersonId vazio")]
    [TestCase(" ", Description = "PersonId vazio")]
    [TestCase("12345", Description = "PersonId muito curto")]
    [TestCase("not-a-guid", Description = "PersonId com formato inválido")]
    [TestCase("550e8400-e29b-41d4-a716-44665544000", Description = "PersonId faltando um caractere")]
    [TestCase("550e8400-e29b-41d4-a716-4466554400000", Description = "PersonId com excesso de caracteres")]
    [TestCase("ZZZZZZZZ-ZZZZ-ZZZZ-ZZZZ-ZZZZZZZZZZZZ", Description = "PersonId com caracteres inválidos")]
    [TestCase("550e8400e29b41d4a716446655440000", Description = "PersonId sem os hífens obrigatórios")]
    [TestCase("{550e8400-e29b-41d4-a716-446655440000}", Description = "PersonId com colchetes inválidos")]
    [TestCase("550E8400-E29B-41D4-G716-446655440000", Description = "PersonId com um caractere fora do intervalo hexadecimal")]
    public void CreateAccount_WhenPersonIdIsNotValid_ThenReturnsAError(string personId)
    {
        // Arrange
        _model.PersonId = personId;
        string errorMessage = "O formato da propriedade PersonId é inválido";

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(error => error.PropertyName == nameof(_model.PersonId) && error.ErrorMessage == errorMessage);
    }
    #endregion PersonId

    #region AccountNumber
    [Test]
    public void CreateAccount_WhenAccountNumberIsNull_ThenReturnsAError()
    {
        // Arrange
        string errorMessage = "A propriedade AccountNumber é obrigatória";
        _model.AccountNumber = null!;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == nameof(_model.AccountNumber) && x.ErrorMessage == errorMessage);
    }
    
    [Test]
    [TestCase("", Description = "AccountNumber vazio")]
    [TestCase(" ", Description = "AccountNumber vazio")]
    [TestCase("12345", Description = "AccountNumber com 5 números")]
    [TestCase("1234567", Description = "AccountNumber com 7 números")]
    [TestCase("123456a", Description = "AccountNumber com letras")]
    [TestCase("123456@", Description = "AccountNumber com caracteres expeciais")]
    public void CreateAccount_WhenAccountNumberIsNotValid_ThenReturnsAError(string accountNumber)
    {
        // Arrange
        _model.AccountNumber = accountNumber;
        string errorMessage = "O formato da propriedade AccountNumber é inválido";

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(error => error.PropertyName == nameof(_model.AccountNumber) && error.ErrorMessage == errorMessage);
    }
    #endregion AccountNumber

    #region Balance
    [Test]
    [TestCase(-1)]
    [TestCase(-50)]
    [TestCase(-100)]
    public void CreateAccount_WhenBalanceIsLessThanZero_ThenReturnsAError(decimal balance)
    {
        // Arrange
        _model.Balance = balance;
        string errorMessage = "O valor da propriedade Balance deve ser maior ou igual a 0";

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(error => error.PropertyName == nameof(_model.Balance) && error.ErrorMessage == errorMessage);
    }
    #endregion Balance
}
