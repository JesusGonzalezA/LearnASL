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

        public async Task DeleteUser(string email)
        {
            UserEntity userDB = await _unitOfWork.UserRepository.GetUserByEmail(email);

            if (userDB == null)
            {
                throw new BusinessException("User does not exists");
            }

            await _unitOfWork.UserRepository.Delete(userDB.Id);
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

        public async Task UpdateTokenEmailConfirmation(string email, string token)
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

            user.TokenEmailConfirmation = token;
            await _unitOfWork.UserRepository.Update(user);
        }

        public async Task UpdateTokenPasswordRecovery(string email, string token)
        {
            UserEntity user = await _unitOfWork.UserRepository.GetUserByEmail(email);

            if (user == null)
            {
                throw new BusinessException("User does not exists");
            }

            user.TokenPasswordRecovery = token;
            await _unitOfWork.UserRepository.Update(user);
        }
    }
}
