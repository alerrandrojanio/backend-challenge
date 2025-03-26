using Challenge.Application.Services;
using Challenge.Domain.DTOs.Person;
using Challenge.Domain.DTOs.Person.Response;
using Challenge.Domain.Interfaces;
using Challenge.Infrastructure.Mapping;
using FluentAssertions;
using Moq;

namespace Challenge.Application.Tests.Services.Person;

public class CreateIndividualPersonServiceTests
{
    private CreateIndividualPersonDTO _createIndividualPersonDTO;
    private CreateIndividualPersonResponseDTO _createIndividualPersonResponseDTO;
    private Mock<IPersonRepository> _mockPersonRepository;
    private Mock<IIndividualPersonRepository> _mockIndividualPersonRepository;
    private Mock<IMerchantPersonRepository> _mockMerchantPersonRepository;
    private Mock<IUnitOfWork> _mockUnitOfWork;

    [SetUp]
    public void SetUp()
    {
        MappingConfig.RegisterMappings();

        _createIndividualPersonDTO = new()
        {
            Name = "Alerrandro",
            CPF = "12345678911",
            Email = "alerrandro@email.com",
            Phone = "(88) 1234-1223",
            BirthDate = DateTime.Parse("07/03/1999")
        };

        _createIndividualPersonResponseDTO = new()
        {
            PersonId = Guid.Parse("1b9ed3c0-a40d-4736-87e1-1f14f78244a7"),
            Name = "Alerrandro",
            CPF = "12345678911",
        };

        _mockPersonRepository = new();
        _mockIndividualPersonRepository = new();
        _mockMerchantPersonRepository = new();
        _mockUnitOfWork = new();

        _mockUnitOfWork.Setup(x => x.BeginTransaction());
        _mockUnitOfWork.Setup(x => x.Commit());
        _mockUnitOfWork.Setup(x => x.Rollback());
    }

    [TearDown]
    public void TearDown()
    {
        _createIndividualPersonDTO = default!;
        _createIndividualPersonResponseDTO = default!;
        _mockPersonRepository = default!;
        _mockIndividualPersonRepository = default!;
        _mockMerchantPersonRepository = default!;
        _mockUnitOfWork = default!;
    }

    [Test]
    public void CreateIndividualPerson_WhenModelIsValid_ThenShouldNotReturnAError()
    {
        // Arrange
        _mockPersonRepository.Setup(x => x.CreatePerson(It.IsAny<Domain.Entities.Person>())).Returns(new Domain.Entities.Person
        {
            Id = Guid.Parse("1b9ed3c0-a40d-4736-87e1-1f14f78244a7"),
            Name = _createIndividualPersonDTO.Name,
            Email = _createIndividualPersonDTO.Email,
            Phone = _createIndividualPersonDTO.Phone
        });

        _mockIndividualPersonRepository.Setup(x => x.CreateIndividualPerson(It.IsAny<Domain.Entities.IndividualPerson>())).Returns(new Domain.Entities.IndividualPerson
        {
            Person = new()
            {
                Id = Guid.Parse("1b9ed3c0-a40d-4736-87e1-1f14f78244a7")
            },
            CPF = _createIndividualPersonDTO.CPF,
            BirthDate = _createIndividualPersonDTO.BirthDate
        });

        PersonService personService = new(_mockPersonRepository.Object, _mockIndividualPersonRepository.Object, _mockMerchantPersonRepository.Object, _mockUnitOfWork.Object);

        // Act
        CreateIndividualPersonResponseDTO? result = personService.CreateIndividualPerson(_createIndividualPersonDTO);

        // Assert
        result.Should().BeEquivalentTo(_createIndividualPersonResponseDTO);
    }

    [Test]
    public void CreateIndividualPerson_WhenModelIsValid_ThenShouldcommitTimesOnce()
    {
        // Arrange
        _mockPersonRepository.Setup(x => x.CreatePerson(It.IsAny<Domain.Entities.Person>())).Returns(new Domain.Entities.Person
        {
            Id = Guid.Parse("1b9ed3c0-a40d-4736-87e1-1f14f78244a7"),
            Name = _createIndividualPersonDTO.Name,
            Email = _createIndividualPersonDTO.Email,
            Phone = _createIndividualPersonDTO.Phone
        });

        _mockIndividualPersonRepository.Setup(x => x.CreateIndividualPerson(It.IsAny<Domain.Entities.IndividualPerson>())).Returns(new Domain.Entities.IndividualPerson
        {
            Person = new()
            {
                Id = Guid.Parse("1b9ed3c0-a40d-4736-87e1-1f14f78244a7")
            },
            CPF = _createIndividualPersonDTO.CPF,
            BirthDate = _createIndividualPersonDTO.BirthDate
        });

        PersonService personService = new(_mockPersonRepository.Object, _mockIndividualPersonRepository.Object, _mockMerchantPersonRepository.Object, _mockUnitOfWork.Object);

        // Act
        CreateIndividualPersonResponseDTO? result = personService.CreateIndividualPerson(_createIndividualPersonDTO);

        // Assert
        _mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
    }

    [Test]
    public void CreateIndividualPerson_WhenRepositoryReturnsAException_ThenShouldRollbackTimesOnce()
    {
        // Arrange
        _mockPersonRepository.Setup(x => x.CreatePerson(It.IsAny<Domain.Entities.Person>())).Returns(new Domain.Entities.Person
        {
            Id = Guid.Parse("1b9ed3c0-a40d-4736-87e1-1f14f78244a7"),
            Name = _createIndividualPersonDTO.Name,
            Email = _createIndividualPersonDTO.Email,
            Phone = _createIndividualPersonDTO.Phone
        });

        _mockIndividualPersonRepository.Setup(x => x.CreateIndividualPerson(It.IsAny<Domain.Entities.IndividualPerson>())).Returns(new Domain.Entities.IndividualPerson
        {
            Person = new()
            {
                Id = Guid.Parse("1b9ed3c0-a40d-4736-87e1-1f14f78244a7")
            },
            CPF = _createIndividualPersonDTO.CPF,
            BirthDate = _createIndividualPersonDTO.BirthDate
        });

        _mockIndividualPersonRepository.Setup(x => x.CreateIndividualPerson(It.IsAny<Domain.Entities.IndividualPerson>())).Throws<Exception>();

        PersonService personService = new(_mockPersonRepository.Object, _mockIndividualPersonRepository.Object, _mockMerchantPersonRepository.Object, _mockUnitOfWork.Object);

        // Act
        Action act = () => personService.CreateIndividualPerson(_createIndividualPersonDTO);

        // Assert
        act.Should().Throw<Exception>();

        _mockUnitOfWork.Verify(x => x.Rollback(), Times.Once);
    }
}
