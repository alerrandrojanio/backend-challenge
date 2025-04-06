using Challenge.API.Mapping;
using Challenge.Application.Services;
using Challenge.Domain.DTOs.User;
using Challenge.Domain.DTOs.User.Response;
using Challenge.Domain.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Challenge.Application.Tests.Services.User;

[TestFixture]
public class CreateUserServiceTests
{
    private CreateUserDTO _createUserDTO;
    private CreateUserResponseDTO _createUserResponseDTO;
    private Mock<IUserRepository> _mockUserRepository;
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private Mock<IPasswordHasher<Domain.Entities.User>> _mockPasswordHasher;

    [SetUp]
    public void SetUp()
    {
        MappingConfig.RegisterMappings();

        _createUserDTO = new()
        {
            Name = "Alerrandro",
            Email = "alerrandro@email.com",
            Password = "12345678"
        };

        _createUserResponseDTO = new()
        {
            UserId = Guid.Parse("1b9ed3c0-a40d-4736-87e1-1f14f78244a7"),
            Name = "Alerrandro"
        };

        _mockUserRepository = new();
        _mockUnitOfWork = new();
        _mockPasswordHasher = new();

        _mockUnitOfWork.Setup(x => x.BeginTransaction());
        _mockUnitOfWork.Setup(x => x.Commit());
        _mockUnitOfWork.Setup(x => x.Rollback());

        _mockPasswordHasher.Setup(x => x.HashPassword(null!, _createUserDTO.Password)).Returns("12345678");
    }

    [TearDown]
    public void TearDown()
    {
        _createUserDTO = default!;
        _createUserResponseDTO = default!;
        _mockUserRepository = new();
        _mockUnitOfWork = new();
        _mockPasswordHasher = new();
    }

    [Test]
    public void CreateUser_WhenModelIsValid_ThenShouldNotReturnAError()
    {
        // Arrange
        _mockUserRepository.Setup(x => x.CreateUser(It.IsAny<Domain.Entities.User>())).Returns(new Domain.Entities.User
        {
            Id = Guid.Parse("1b9ed3c0-a40d-4736-87e1-1f14f78244a7"),
            Name = _createUserDTO.Name,
            Email = _createUserDTO.Email,
            PasswordHash = "12345678",
            CreatedAt = DateTime.Now
        });

        UserService userService = new(_mockUserRepository.Object, _mockUnitOfWork.Object, _mockPasswordHasher.Object);
        
        // Act
        CreateUserResponseDTO? result = userService.CreateUser(_createUserDTO);
        
        // Assert
        result.Should().BeEquivalentTo(_createUserResponseDTO);
    }

    [Test]
    public void CreateUser_WhenModelIsValid_ThenShouldCommitTimesOnce()
    {
        // Arrange
        _mockUserRepository.Setup(x => x.CreateUser(It.IsAny<Domain.Entities.User>())).Returns(new Domain.Entities.User
        {
            Id = Guid.Parse("1b9ed3c0-a40d-4736-87e1-1f14f78244a7"),
            Name = _createUserDTO.Name,
            Email = _createUserDTO.Email,
            PasswordHash = "12345678",
            CreatedAt = DateTime.Now
        });

        UserService userService = new(_mockUserRepository.Object, _mockUnitOfWork.Object, _mockPasswordHasher.Object);

        // Act
        CreateUserResponseDTO? result = userService.CreateUser(_createUserDTO);

        // Assert
        _mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
    }

    [Test]
    public void CreateUser_WhenRepositoryReturnsAException_ThenShouldRollbackTimesOnce()
    {
        // Arrange
        _mockUserRepository.Setup(x => x.CreateUser(It.IsAny<Domain.Entities.User>())).Returns(new Domain.Entities.User
        {
            Id = Guid.Parse("1b9ed3c0-a40d-4736-87e1-1f14f78244a7"),
            Name = _createUserDTO.Name,
            Email = _createUserDTO.Email,
            PasswordHash = "12345678",
            CreatedAt = DateTime.Now
        });

        _mockUserRepository.Setup(x => x.CreateUser(It.IsAny<Domain.Entities.User>())).Throws<Exception>();

        UserService userService = new(_mockUserRepository.Object, _mockUnitOfWork.Object, _mockPasswordHasher.Object);

        // Act
        Action act = () => userService.CreateUser(_createUserDTO);

        // Assert
        act.Should().Throw<Exception>();

        _mockUnitOfWork.Verify(x => x.Rollback(), Times.Once);
    }
}
