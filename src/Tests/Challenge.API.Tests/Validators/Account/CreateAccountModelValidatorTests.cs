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
    public void CreateAccount_WhenModelIsValid_ThenNotReturnErrors()
    {
        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public void CreateAccount_WhenPersonIdIsNull_ThenReturnError()
    {
        // Arrange
        string errorMessage = "A propriedade PersonId é obrigatória";
        _model.PersonId = null!;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().ContainSingle(error => error.ErrorMessage == errorMessage);
    }

    [Test]
    [TestCase("")]
    [TestCase("12345", Description = "GUID muito curto")]
    [TestCase("not-a-guid", Description = "Formato inválido")]
    [TestCase("550e8400-e29b-41d4-a716-44665544000", Description = "Faltando um caractere")]
    [TestCase("550e8400-e29b-41d4-a716-4466554400000", Description = "Excesso de caracteres")]
    [TestCase("ZZZZZZZZ-ZZZZ-ZZZZ-ZZZZ-ZZZZZZZZZZZZ", Description = "Contém caracteres inválidos")]
    [TestCase("550e8400e29b41d4a716446655440000", Description = "Sem os hífens obrigatórios")]
    [TestCase("{550e8400-e29b-41d4-a716-446655440000}", Description = "Contém colchetes inválidos")]
    [TestCase("550E8400-E29B-41D4-G716-446655440000", Description = "Contém um caractere fora do intervalo hexadecimal")]
    public void CreateAccount_WhenPersonIdIsNotValid_ThenReturnError(string? personId)
    {
        // Arrange
        string errorMessage = "A propriedade PersonId é inválida";
        _model.PersonId = personId!;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().ContainSingle().Which.ErrorMessage.Should().Be(errorMessage);
    }
}
