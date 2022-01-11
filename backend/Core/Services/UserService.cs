using System;
using System.Threading.Tasks;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService
        (
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
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

            return await _unitOfWork.UserRepository.Add(user);
        }

        public async Task DeleteUser(Guid id)
        {
            UserEntity userDB = await _unitOfWork.UserRepository.GetById(id);

            if (userDB == null)
            {
                throw new BusinessException("User does not exist");
            }

            await _unitOfWork.UserRepository.Delete(id);
        }

        public async Task<bool> CheckConfirmedUser(string email)
        {
            UserEntity user = await _unitOfWork.UserRepository.GetUserByEmail(email);
            
            if (user == null)
            {
                throw new BusinessException("User does not exist");
            }

            return user.ConfirmedEmail;
        }

        public async Task ChangePassword(string email, string password, string token)
        {
            UserEntity user = await _unitOfWork.UserRepository.GetUserByEmail(email);

            if (user == null)
            {
                throw new BusinessException("User does not exist");
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
                throw new BusinessException("User does not exist");
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

        public async Task UpdateTokenEmailConfirmation(string email, string token)
        {
            UserEntity user = await _unitOfWork.UserRepository.GetUserByEmail(email);

            if (user == null)
            {
                throw new BusinessException("User does not exist");
            }

            if (user.ConfirmedEmail)
            {
                throw new BusinessException("Email already confirmed");
            }

            user.TokenEmailConfirmation = token;
            await _unitOfWork.UserRepository.Update(user);
        }

        public async Task UpdateTokenPasswordRecovery(string email, string token)
        {
            UserEntity user = await _unitOfWork.UserRepository.GetUserByEmail(email);

            if (user == null)
            {
                throw new BusinessException("User does not exist");
            }

            user.TokenPasswordRecovery = token;
            await _unitOfWork.UserRepository.Update(user);
        }

        public async Task ChangeEmail(string oldEmail, string newEmail)
        {
            UserEntity user = await _unitOfWork.UserRepository.GetUserByEmail(oldEmail);

            if (user == null)
            {
                throw new BusinessException("User does not exist");
            }

            if (await _unitOfWork.UserRepository.GetUserByEmail(newEmail) != null)
            {
                throw new BusinessException("Email already in use");
            }

            user.Email = newEmail;
            user.ConfirmedEmail = false;
            await _unitOfWork.UserRepository.Update(user);
        }
    }
}
