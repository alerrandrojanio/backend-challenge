using Challenge.API.Models.Account;
using Challenge.API.Validators.Account;
using FluentAssertions;
using FluentValidation.Results;

namespace Challenge.API.Tests.Validators.Account;

[TestFixture]
public class CreateTransferModelValidatorTests
{
    private CreateTransferModel _model;
    private CreateTransferModelValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _model = new()
        {
            Value = 100,
            PayerId = Guid.NewGuid().ToString(),
            PayeeId = Guid.NewGuid().ToString()
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
    public void CreateTransfer_WhenModelIsValid_ThenShouldNotReturnAError()
    {
        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    #region PayerId
    [Test]
    public void CreateTransfer_WhenPayerIdIsNull_ThenShouldReturnAError()
    {
        // Arrange
        string errorMessage = "A propriedade PayerId é obrigatória";
        _model.PayerId = null!;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == nameof(_model.PayerId) && x.ErrorMessage == errorMessage);
    }

    [Test]
    [TestCase("", Description = "PayerId vazio")]
    [TestCase(" ", Description = "PayerId vazio")]
    [TestCase("12345", Description = "PayerId muito curto")]
    [TestCase("not-a-guid", Description = "PayerId com formato inválido")]
    [TestCase("550e8400-e29b-41d4-a716-44665544000", Description = "PayerId faltando um caractere")]
    [TestCase("550e8400-e29b-41d4-a716-4466554400000", Description = "PayerId com excesso de caracteres")]
    [TestCase("ZZZZZZZZ-ZZZZ-ZZZZ-ZZZZ-ZZZZZZZZZZZZ", Description = "PayerId com caracteres inválidos")]
    [TestCase("550e8400e29b41d4a716446655440000", Description = "PayerId sem os hífens obrigatórios")]
    [TestCase("{550e8400-e29b-41d4-a716-446655440000}", Description = "PayerId com colchetes inválidos")]
    [TestCase("550E8400-E29B-41D4-G716-446655440000", Description = "PayerId com um caractere fora do intervalo hexadecimal")]
    public void CreateTransfer_WhenPayerIdIsNotValid_ThenShouldReturnAError(string payerId)
    {
        // Arrange
        _model.PayerId = payerId;
        string errorMessage = "O formato da propriedade PayerId é inválido";

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(error => error.PropertyName == nameof(_model.PayerId) && error.ErrorMessage == errorMessage);
    }
    #endregion PayerId

    #region PayeeId
    [Test]
    public void CreateTransfer_WhenPayeeIdIsNull_ThenShouldReturnAError()
    {
        // Arrange
        string errorMessage = "A propriedade PayeeId é obrigatória";
        _model.PayeeId = null!;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == nameof(_model.PayeeId) && x.ErrorMessage == errorMessage);
    }

    [Test]
    [TestCase("", Description = "PayeeId vazio")]
    [TestCase(" ", Description = "PayeeId vazio")]
    [TestCase("12345", Description = "PayeeId muito curto")]
    [TestCase("not-a-guid", Description = "PayeeId com formato inválido")]
    [TestCase("550e8400-e29b-41d4-a716-44665544000", Description = "PayeeId faltando um caractere")]
    [TestCase("550e8400-e29b-41d4-a716-4466554400000", Description = "PayeeId com excesso de caracteres")]
    [TestCase("ZZZZZZZZ-ZZZZ-ZZZZ-ZZZZ-ZZZZZZZZZZZZ", Description = "PayeeId com caracteres inválidos")]
    [TestCase("550e8400e29b41d4a716446655440000", Description = "PayeeId sem os hífens obrigatórios")]
    [TestCase("{550e8400-e29b-41d4-a716-446655440000}", Description = "PayeeId com colchetes inválidos")]
    [TestCase("550E8400-E29B-41D4-G716-446655440000", Description = "PayeeId com um caractere fora do intervalo hexadecimal")]
    public void CreateTransfer_WhenPayeeIdIsNotValid_ThenShouldReturnAError(string payeeId)
    {
        // Arrange
        _model.PayeeId = payeeId;
        string errorMessage = "O formato da propriedade PayeeId é inválido";

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(error => error.PropertyName == nameof(_model.PayeeId) && error.ErrorMessage == errorMessage);
    }
    #endregion PayeeId
}
