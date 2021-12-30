using System;
using System.Threading.Tasks;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Services;
using Tests.Mocks;
using Xunit;

namespace Tests.Core.Services
{
    public partial class TestUserService
    {
        [Fact]
        public async Task UserService_GetUser()
        {
            UserService userService = new UserService(_unitOfWork);
            UserEntity userEntity = new UserEntity("test@mail.com", "test");
            Guid guid = new Guid();

            UserEntity userEntityDB = await userService.GetUser(guid);
            Assert.Null(userEntityDB);

            guid = await userService.AddUser(userEntity);
            userEntityDB = await userService.GetUser(guid);
            Assert.NotNull(userEntityDB);
        }

        [Theory]
        [InlineData("test@mail.com")]
        public async Task UserService_GetUserByEmail(string email)
        {
            UserService userService = new UserService(_unitOfWork);
            UserEntity userEntity = new UserEntity(email, "test");

            await userService.AddUser(userEntity);
        }
        
        [Theory]
        [InlineData("test@mail.com", "test")]
        public async Task UserService_AddUser_ReturnsGuid(string email, string password)
        {
            UserService userService = new UserService(_unitOfWork);
            UserEntity userEntity = new UserEntity(email, password);

            await userService.AddUser(userEntity);
        }

        [Theory]
        [InlineData("test@mail.com", "test")]
        public async Task UserService_AddUser_ThrowsBEUserAlreadyExists(string email, string password)
        {
            UserService userService = new UserService(_unitOfWork);
            UserEntity userEntity = new UserEntity(email, password);

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await userService.AddUser(userEntity);
                await userService.AddUser(userEntity);
            });
        }

        
        [Theory]
        [InlineData("test@mail.com")]
        public async Task UserService_DeleteUser_DeletesUser(string email)
        {
            UserService userService = new UserService(_unitOfWork);
            UserEntity userEntity = new UserEntity(email, "test");

            await userService.AddUser(userEntity);
            await userService.DeleteUser(email);
        }

        [Theory]
        [InlineData("test@mail.com")]
        public async Task UserService_DeleteUser_ThrowsBEUserDoesNotExist(string email)
        {
            UserService userService = new UserService(_unitOfWork);

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await userService.DeleteUser(email);
            });
        }

        [Theory]
        [InlineData("test@mail.com")]
        public async Task UserService_CheckConfirmedUser_ThrowsCheckConfirmedUser(string email)
        {
            UserService userService = new UserService(_unitOfWork);
            UserEntity userEntity = new UserEntity(email, "test");

            await userService.AddUser(userEntity);

            bool isConfirmed = await userService.CheckConfirmedUser(email);
            Assert.False(isConfirmed);

            userEntity.ConfirmedEmail = true;
            isConfirmed = await userService.CheckConfirmedUser(email);
            Assert.True(isConfirmed);
        }

        [Theory]
        [InlineData("test@mail.com")]
        public async Task UserService_CheckConfirmedUser_ThrowsBEUserDoesNotExist(string email)
        {
            UserService userService = new UserService(_unitOfWork);

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await userService.CheckConfirmedUser(email);
            });
        }

        
        [Theory]
        [InlineData("test@mail.com")]
        public async Task UserService_ChangePassword_ThrowsBEUserDoesNotExist(string email)
        {
            UserService userService = new UserService(_unitOfWork);

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await userService.ChangePassword(email, "test", "token");
            });
        }

        [Theory]
        [InlineData("test@mail.com", "testPass", "testPassUpdated")]
        public async Task UserService_ChangePassword_ThrowsBETokenNoLongerValid(string email, string password, string newPassword)
        {
            UserService userService = new UserService(_unitOfWork);
            UserEntity userEntity = new UserEntity(email, password);

            await userService.AddUser(userEntity);

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await userService.ChangePassword(email, newPassword, "token");
            });
        }

        [Theory]
        [InlineData("test@mail.com", "testPass", "testPassUpdated")]
        public async Task UserService_ChangePassword_ChangesPassword(string email, string password, string newPassword)
        {
            UserService userService = new UserService(_unitOfWork);
            UserEntity userEntity = new UserEntity(email, password);

            await userService.AddUser(userEntity);
            await userService.UpdateTokenPasswordRecovery(email, "token");
            await userService.ChangePassword(
                email,
                newPassword,
                userEntity.TokenPasswordRecovery
            ) ;

            Assert.NotStrictEqual(password, userEntity.Password);
            Assert.Null(userEntity.TokenPasswordRecovery);
        }

        [Theory]
        [InlineData("test@mail.com")]
        public async Task UserService_ConfirmEmail_ThrowsBEUserDoesNotExist(string email)
        {
            UserService userService = new UserService(_unitOfWork);

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await userService.ConfirmEmail(email, "token");
            });
        }

        [Theory]
        [InlineData("test@mail.com")]
        public async Task UserService_ConfirmEmail_ThrowsBETokenNoLongerValid(string email)
        {
            UserService userService = new UserService(_unitOfWork);
            UserEntity userEntity = new UserEntity(email, "test");

            await userService.AddUser(userEntity);

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await userService.ConfirmEmail(email, "token");
            });
        }

        [Theory]
        [InlineData("test@mail.com")]
        public async Task UserService_ConfirmEmail_ConfirmsEmail(string email)
        {
            UserService userService = new UserService(_unitOfWork);
            UserEntity userEntity = new UserEntity(email, "test");

            await userService.AddUser(userEntity);
            await userService.UpdateTokenEmailConfirmation(email, "token");
            await userService.ConfirmEmail(email, userEntity.TokenEmailConfirmation);

            Assert.True(userEntity.ConfirmedEmail);
            Assert.Null(userEntity.TokenEmailConfirmation);
        }

        [Theory]
        [InlineData("test@mail.com", "token")]
        public async Task UserService_UpdateTokenEmailConfirmation_ThrowsBEUserDoesNotExist(string email, string token)
        {
            UserService userService = new UserService(_unitOfWork);

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await userService.UpdateTokenEmailConfirmation(email, token);
            });
        }

        [Theory]
        [InlineData("test@mail.com", "token")]
        public async Task UserService_UpdateTokenEmailConfirmation_ThrowsBEEmailAlreadyConfirmed(string email, string token)
        {
            UserService userService = new UserService(_unitOfWork);
            UserEntity userEntity = new UserEntity(email, "test");

            await userService.AddUser(userEntity);
            await userService.UpdateTokenEmailConfirmation(email, token);
            await userService.ConfirmEmail(email, userEntity.TokenEmailConfirmation);

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await userService.UpdateTokenEmailConfirmation(email, token);
            });
        }

        [Theory]
        [InlineData("test@mail.com", "token")]
        public async Task UserService_UpdateTokenEmailConfirmation_UpdatesTokenEmailConfirmation(string email, string token)
        {
            UserService userService = new UserService(_unitOfWork);
            UserEntity userEntity = new UserEntity(email, "test");
            string oldToken = userEntity.TokenEmailConfirmation;

            await userService.AddUser(userEntity);
            await userService.UpdateTokenEmailConfirmation(email, token);

            Assert.NotSame(oldToken, userEntity.TokenEmailConfirmation);
        }

        [Theory]
        [InlineData("test@mail.com", "token")]
        public async Task UserService_UpdateTokenPasswordRecovery_ThrowsBEUserDoesNotExist(string email, string token)
        {
            UserService userService = new UserService(_unitOfWork);

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await userService.UpdateTokenPasswordRecovery(email, token);
            });
        }

        [Theory]
        [InlineData("test@mail.com", "token")]
        public async Task UserService_UpdateTokenPasswordRecovery_UpdatesTokenPasswordRecovery(string email, string token)
        {
            UserService userService = new UserService(_unitOfWork);
            UserEntity userEntity = new UserEntity(email, "test");
            string oldToken = userEntity.TokenPasswordRecovery;

            await userService.AddUser(userEntity);
            await userService.UpdateTokenPasswordRecovery(email, token);

            Assert.NotSame(oldToken, userEntity.TokenPasswordRecovery);
        }
    }

    public partial class TestUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TestUserService()
        {
            _unitOfWork = MockUnitOfWork.GetMockUnitOfWork();
        }
    }
}
