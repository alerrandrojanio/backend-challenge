using Challenge.API.Models.Person;
using Challenge.API.Validators.Person;
using FluentAssertions;
using FluentValidation.Results;
using NUnit.Framework;

namespace Challenge.API.Tests.Validators.Person;

[TestFixture]
public class CreateIndividualPersonModelValidatorTests
{
    private CreateIndividualPersonModel _model;
    private CreateIndividualPersonModelValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _model = new()
        {
            Name = "Alerrandro",
            Email = "alerrandro@email.com",
            Phone = "(88) 91234-9889",
            CPF = "06991384003",
            BirthDate = "07/03/1999"
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
    public void CreateIndividualPerson_WhenModelIsValid_ThenShouldNotReturnsErrors()
    {
        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    #region Name
    [Test]
    public void CreateIndividualPerson_WhenNameIsNull_ThenShouldReturnAError()
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
    public void CreateIndividualPerson_WhenNameLengthIsLessThanThree_ThenShouldReturnAError(string name)
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
    public void CreateIndividualPerson_WhenEmailIsNull_ThenShouldReturnAError()
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
    public void CreateIndividualPerson_WhenEmailIsNotValid_ThenShouldReturnAError(string email)
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
    public void CreateIndividualPerson_WhenPhoneIsNull_ThenShouldReturnAError()
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
    public void CreateIndividualPerson_WhenPhoneIsNotValid_ThenShouldReturnAError(string phone)
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

    #region CPF
    [Test]
    public void CreateIndividualPerson_WhenCPFIsNull_ThenShouldReturnAError()
    {
        // Arrange
        string errorMessage = "A propriedade CPF é obrigatória";
        _model.CPF = null!;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == nameof(_model.CPF) && x.ErrorMessage == errorMessage);
    }

    [Test]
    [TestCase("1234567891", Description = "CPF com 10 digitos")]
    [TestCase("123456789123", Description = "CPF com 12 digitos")]
    public void CreateIndividualPerson_WhenCPFLengthIsNotValid_ThenShouldReturnAError(string cpf)
    {
        // Arrange
        string errorMessage = "A propriedade CPF deve tamanho exato de 11 caractere(s)";
        _model.CPF = cpf;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == nameof(_model.CPF) && x.ErrorMessage == errorMessage);
    }

    [Test]
    [TestCase("1234567891a", Description = "CPF com 11 digitos sendo 1 caractere")]
    [TestCase("1234567891@", Description = "CPF com 11 digitos sendo 1 caractere especial")]
    [TestCase("abcdefghijk", Description = "CPF com 11 caracteres")]
    public void CreateIndividualPerson_WhenCPFIsNotValid_ThenShouldReturnAError(string cpf)
    {
        // Arrange
        string errorMessage = "A propriedade CPF deve conter apenas digitos";
        _model.CPF = cpf;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == nameof(_model.CPF) && x.ErrorMessage == errorMessage);
    }
    #endregion CPF

    #region BirthDate
    [Test]
    public void CreateIndividualPerson_WhenBirthDateIsNull_ThenShouldReturnAError()
    {
        // Arrange
        string errorMessage = "A propriedade BirthDate é obrigatória";
        _model.BirthDate = null!;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == nameof(_model.BirthDate) && x.ErrorMessage == errorMessage);
    }

    [Test]
    [TestCase("32/13/2023", Description = "BirthDate com data inexistente")]
    [TestCase("not-a-date", Description = "BirthDate com formato inválido")]
    [TestCase("11-99-2024", Description = "BirthDate com mês inválido")]
    [TestCase("32-12-2024", Description = "BirthDate com dia inválido")]
    public void CreateIndividualPerson_WhenBirthDateIsNotValid_ThenShouldReturnAError(string birthDate)
    {
        // Arrange
        string errorMessage = "A data repassada em BithDate é inválida";
        _model.BirthDate = birthDate;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == nameof(_model.BirthDate) && x.ErrorMessage == errorMessage);
    }

    [Test]
    [TestCase("2100-01-01", Description = "BirthDate com data no futuro")]
    [TestCase("3000-12-31", Description = "BirthDate com data muito no futuro")]
    public void CreateIndividualPerson_WhenBirthDateIsInTheFuture_ThenShouldReturnAError(string birthDate)
    {
        // Arrange
        string errorMessage = "A valor da propriedade BithDate não pode ser maior que a data atual";
        _model.BirthDate = birthDate;

        // Act
        ValidationResult result = _validator.Validate(_model);

        // Assert
        result.Errors.Should().Contain(x => x.PropertyName == nameof(_model.BirthDate) && x.ErrorMessage == errorMessage);
    }
    #endregion BirthDate
}
