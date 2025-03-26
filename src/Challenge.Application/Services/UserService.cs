using Challenge.Application.Resources;
using Challenge.Domain.DTOs.User;
using Challenge.Domain.DTOs.User.Response;
using Challenge.Domain.Entities;
using Challenge.Domain.Exceptions;
using Challenge.Domain.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Challenge.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public CreateUserResponseDTO? CreateUser(CreateUserDTO createUserDTO)
    {
        CreateUserResponseDTO? createUserResponseDTO = null;

        try
        {
            string passwordHash = _passwordHasher.HashPassword(null!, createUserDTO.Password);

            User user = ValueTuple.Create(createUserDTO, passwordHash).Adapt<User>();

            _unitOfWork.BeginTransaction();

            user = _userRepository.CreateUser(user);

            createUserResponseDTO = user.Adapt<CreateUserResponseDTO>();

            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }

        return createUserResponseDTO;
    }

    public void ValidateUser(Guid userId, string password)
    {
        User? user = _userRepository.GetUserById(userId);

        if (user is null)
            throw new ServiceException(ResourceMsg.User_Validade_Fail, HttpStatusCode.BadRequest);

        PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(null!, user.PasswordHash, password);

        if (result != PasswordVerificationResult.Success)
            throw new ServiceException(ResourceMsg.User_Validade_Fail, HttpStatusCode.BadRequest);
    }
}
