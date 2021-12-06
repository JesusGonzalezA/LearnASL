using Core.Interfaces;
using System.Threading.Tasks;
using System;
using Core.Entities;
using Core.Exceptions;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ITokenService _tokenService;

        public UserService
        (
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            ITokenService tokenService
        )
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _tokenService = tokenService;
        }

        public async Task<UserEntity> GetUser(Guid id)
        {
            return await _unitOfWork.UserRepository.GetById(id);
        }

        public async Task<UserEntity> GetUserByEmail(string email)
        {
            return await _unitOfWork.UserRepository.GetUserByEmail(email);
        }

        public async Task<Guid> AddUser(UserEntity user)
        {
            UserEntity userDB = await _unitOfWork.UserRepository.GetUserByEmail(user.Email);

            if (userDB != null)
            {
                throw new BusinessException("User already exist");
            }

            await _emailService.SendRegistrationEmail(user.Email);
            return await _unitOfWork.UserRepository.Add(user);
        }

        public async Task DeleteUser(string email)
        {
            UserEntity userDB = await _unitOfWork.UserRepository.GetUserByEmail(email);

            if (userDB == null)
            {
                throw new BusinessException("User does not exists");
            }

            await _unitOfWork.UserRepository.Delete(userDB.Id);
        }

        public async Task<bool> CheckCredentials(string email, string password)
        {
            UserEntity user = await _unitOfWork.UserRepository.GetUserByEmail(email);

            if ( user == null )
            {
                throw new BusinessException("User does not exists");
            }

            return user.Password.Equals(password);
        }

        public async Task<bool> CheckConfirmedUser(string email)
        {
            UserEntity user = await _unitOfWork.UserRepository.GetUserByEmail(email);
            
            if (user == null)
            {
                throw new BusinessException("User does not exists");
            }

            return user.ConfirmedEmail;
        }

        public async Task ChangePassword(string email, string password, string token)
        {
            UserEntity user = await _unitOfWork.UserRepository.GetUserByEmail(email);

            if (user == null)
            {
                throw new BusinessException("User does not exists");
            }
            if
            (
                user.TokenPasswordRecovery == null ||
                !user.TokenPasswordRecovery.Equals(token)
            )
            {
                throw new BusinessException("Token no longer valid");
            }

            user.Password = password;
            user.TokenPasswordRecovery = null;
            await _unitOfWork.UserRepository.Update(user);
        }

        public async Task ConfirmEmail(string email, string token)
        {
            UserEntity user = await _unitOfWork.UserRepository.GetUserByEmail(email);

            if (user == null)
            {
                throw new BusinessException("User does not exists");
            }
            if
            (
                user.TokenEmailConfirmation == null ||
                !user.TokenEmailConfirmation.Equals(token)
            )
            {
                throw new BusinessException("Token no longer valid");
            }

            user.ConfirmedEmail = true;
            user.TokenEmailConfirmation = null;
            await _unitOfWork.UserRepository.Update(user);
        }

        public async Task<string> RegenerateTokenEmailConfirmation(string email)
        {
            UserEntity user = await _unitOfWork.UserRepository.GetUserByEmail(email);

            if (user == null)
            {
                throw new BusinessException("User does not exists");
            }
            if (user.ConfirmedEmail)
            {
                throw new BusinessException("Email already confirmed");
            }

            string token = _tokenService.GenerateJWTToken();
            user.TokenEmailConfirmation = token;
            await _unitOfWork.UserRepository.Update(user);

            return token;
        }

        public async Task<string> RegenerateTokenPasswordRecovery(string email)
        {
            UserEntity user = await _unitOfWork.UserRepository.GetUserByEmail(email);

            if (user == null)
            {
                throw new BusinessException("User does not exists");
            }

            string token = _tokenService.GenerateJWTToken();
            user.TokenPasswordRecovery = token;
            await _unitOfWork.UserRepository.Update(user);

            return token;
        }
    }
}
