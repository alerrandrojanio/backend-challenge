using Challenge.API.Models.User;
using Challenge.API.Validators.User;
using FluentAssertions;
using FluentValidation.Results;

namespace Challenge.API.Tests.Validators.User;

[TestFixture]
public class CreateUserModelValidatorTests
{
    private CreateUserModel _model;
    private CreateUserModelValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _model = new()
        {
            Name = "Alerrandro",
            Email = "alerrandro@email.com",
            Password = "12345678"
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
    public void CreateUser_WhenModelIsValid_ThenNotReturnsErrors()
    {
        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    #region Name
    [Test]
    public void CreateUser_WhenNameIsNull_ThenReturnsAError()
    {
        // Arrange
        string errorMessage = "A propriedade Name é obrigatória";
        _model.Name = null!;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == nameof(_model.Name) && x.ErrorMessage == errorMessage);
    }

    [Test]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("a")]
    [TestCase("ab")]
    public void CreateUser_WhenNameLengthIsLessThanThree_ThenReturnsAError(string name)
    {
        // Arrange
        string errorMessage = "A propriedade Name deve ter pelo menos 3 caractere(s)";
        _model.Name = name;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == nameof(_model.Name) && x.ErrorMessage == errorMessage);
    }
    #endregion Name

    #region Email
    [Test]
    public void CreateUser_WhenEmailIsNull_ThenReturnsAError()
    {
        // Arrange
        string errorMessage = "A propriedade Email é obrigatória";
        _model.Email = null!;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == nameof(_model.Email) && x.ErrorMessage == errorMessage);
    }

    [Test]
    [TestCase("", Description = "Email vazio")]
    [TestCase(" ", Description = "Email vazio")]
    [TestCase("plainaddress", Description = "Email sem @ e domínio")]
    [TestCase("@no-local-part.com", Description = "Email sem parte local antes do @")]
    [TestCase("user@.com", Description = "Email sem nome de domínio")]
    [TestCase("user@domain", Description = "Email sem TLD (top-level domain)")]
    [TestCase("user@domain.c", Description = "Email com TLD menor que 2 caracteres")]
    [TestCase("user@domain.com.", Description = "Email com domínio terminado com ponto")]
    [TestCase("user@domain@domain.com", Description = "Email com dois @ no endereço")]
    [TestCase("user domain.com", Description = "Email sem @, espaço no lugar")]
    [TestCase("user@do_main.com", Description = "Email com caracter _ inválido no domínio")]
    [TestCase("usér@domain.com", Description = "Email com caracter especial inválido (acentuação)")]
    public void CreateUser_WhenEmailIsNotValid_ThenReturnsAError(string email)
    {
        // Arrange
        string errorMessage = "A propriedade Email está fora do padrão correto";
        _model.Email = email;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == nameof(_model.Email) && x.ErrorMessage == errorMessage);
    }
    #endregion Email

    #region Password
    [Test]
    public void CreateUser_WhenPasswordIsNull_ThenReturnsAError()
    {
        // Arrange
        string errorMessage = "A propriedade Password é obrigatória";
        _model.Password = null!;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == nameof(_model.Password) && x.ErrorMessage == errorMessage);
    }

    [Test]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("a")]
    [TestCase("ab")]
    [TestCase("abcdefg")]
    public void CreateUser_WhenPasswordLengthIsLessThanEight_ThenReturnsAError(string password)
    {
        // Arrange
        string errorMessage = "A propriedade Password deve ter pelo menos 8 caractere(s)";
        _model.Password = password;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == nameof(_model.Password) && x.ErrorMessage == errorMessage);
    }
    #endregion Password
}
