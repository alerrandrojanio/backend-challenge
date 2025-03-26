using Challenge.API.Models.Person;
using Challenge.API.Validators.Person;
using FluentAssertions;
using FluentValidation.Results;

namespace Challenge.API.Tests.Validators.Person;

[TestFixture]
public class CreateMerchantPersonModelValidatorTests
{
    private CreateMerchantPersonModel _model;
    private CreateMerchantPersonModelValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _model = new()
        {
            Name = "Alerrandro",
            Email = "alerrandro@email.com",
            Phone = "(88) 91234-9889",
            CNPJ = "39974099000168",
            MerchantName = "Fábrica",
            MerchantAddress = "Rua rua, 1234",
            MerchantContact = "(85) 1234-4321"
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
    public void CreateMerchantPerson_WhenModelIsValid_ThenShouldNotReturnsErrors()
    {
        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    #region Name
    [Test]
    public void CreateMerchantPerson_WhenNameIsNull_ThenShouldReturnAError()
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
    public void CreateMerchantPerson_WhenNameLengthIsLessThanThree_ThenShouldReturnAError(string name)
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
    public void CreateMerchantPerson_WhenEmailIsNull_ThenShouldReturnAError()
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
    public void CreateMerchantPerson_WhenEmailIsNotValid_ThenShouldReturnAError(string email)
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

    #region Phone
    [Test]
    public void CreateMerchantPerson_WhenPhoneIsNull_ThenShouldReturnAError()
    {
        // Arrange
        string errorMessage = "A propriedade Phone é obrigatória";
        _model.Phone = null!;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == nameof(_model.Phone) && x.ErrorMessage == errorMessage);
    }

    [Test]
    [TestCase("1234", Description = "Phone com número muito curto")]
    [TestCase("999999999999", Description = "Phone com número muito longo")]
    [TestCase("abcdefghij", Description = "Phone com caracteres não numéricos")]
    [TestCase("123-456-7890", Description = "Phone com formato inválido para telefone brasileiro")]
    [TestCase("(99)123-4567", Description = "Phone com número fixo incompleto")]
    [TestCase("99 99999-999", Description = "Phone com número móvel faltando um dígito")]
    [TestCase("(99) 9999-999a", Description = "Phone com caractere inválido")]
    public void CreateMerchantPerson_WhenPhoneIsNotValid_ThenShouldReturnAError(string phone)
    {
        // Arrange
        string errorMessage = "A propriedade Phone deve seguir o formato (xx) xxxxx-xxxx";
        _model.Phone = phone;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == nameof(_model.Phone) && x.ErrorMessage == errorMessage);
    }
    #endregion Phone

    #region CNPJ
    [Test]
    public void CreateIndividualPerson_WhenCNPJIsNull_ThenShouldReturnAError()
    {
        // Arrange
        string errorMessage = "A propriedade CNPJ é obrigatória";
        _model.CNPJ = null!;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == nameof(_model.CNPJ) && x.ErrorMessage == errorMessage);
    }

    [Test]
    [TestCase("1234567891234", Description = "CNPJ com 13 digitos")]
    [TestCase("123456789123456", Description = "CNPJ com 15 digitos")]
    public void CreateIndividualPerson_WhenCNPJLengthIsNotValid_ThenShouldReturnAError(string cnpj)
    {
        // Arrange
        string errorMessage = "A propriedade CNPJ deve tamanho exato de 14 caractere(s)";
        _model.CNPJ = cnpj;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == nameof(_model.CNPJ) && x.ErrorMessage == errorMessage);
    }

    [Test]
    [TestCase("1234567891234a", Description = "CPF com 14 digitos sendo 1 caractere")]
    [TestCase("1234567891234@", Description = "CPF com 14 digitos sendo 1 caractere especial")]
    [TestCase("abcdefghijklmn", Description = "CPF com 14 caracteres")]
    public void CreateIndividualPerson_WhenCNPJIsNotValid_ThenShouldReturnAError(string cnpj)
    {
        // Arrange
        string errorMessage = "A propriedade CNPJ deve conter apenas digitos";
        _model.CNPJ = cnpj;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == nameof(_model.CNPJ) && x.ErrorMessage == errorMessage);
    }
    #endregion CNPJ

    #region MerchantName
    #endregion MerchantName

    #region MerchantAddress
    #endregion MerchantAddress

    #region MerchantContact
    #endregion MerchantContact
}
