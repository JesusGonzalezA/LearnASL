using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Tests;
using Core.Enums;
using Core.Interfaces;
using Core.Options;
using Core.Services;
using Infraestructure.Interfaces;
using Infraestructure.Services;
using Microsoft.Extensions.Options;
using Tests.Mocks;
using Xunit;

namespace Tests.Core.Services
{
    public partial class TestQuestionService
    {
        [Theory]
        [MemberData(nameof(AllTestTypes))]
        public async Task QuestionService_AddQuestions_AddsCorrectly(TestType testType)
        {
            BaseQuestionEntity question = _questionGeneratorService.CreateQuestion(testType, Difficulty.EASY, Guid.Empty);
        }

    }

    public partial class TestQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IQuestionGeneratorService _questionGeneratorService;

        public TestQuestionService()
        {
            _unitOfWork = MockUnitOfWork.GetMockUnitOfWork();
            _paginationOptions = new PaginationOptions()
            {
                DefaultPageNumber = 1,
                DefaultPageSize = 10,
                MaximumPageSize = 20
            };
            _questionGeneratorService = new QuestionGeneratorService();
        }

        public static IEnumerable<object[]> AllTestTypes()
        {
            foreach (var number in Enum.GetValues(typeof(TestType)))
            {
                yield return new object[] { number };
            }
        }

        private TestService CreateTestService()
        {
            IQuestionService questionService = new QuestionService(_unitOfWork);
            IOptions<PaginationOptions> paginationOptions = Options.Create(_paginationOptions);
            return new TestService(_unitOfWork, questionService, paginationOptions);
        }

        private UserService CreateUserService()
        {
            return new UserService(_unitOfWork);
        }

        private async Task<Guid> AddUser()
        {
            UserService userService = CreateUserService();
            UserEntity userEntity = new UserEntity("test@mail.com", "test");

            return await userService.AddUser(userEntity);
        }

        private async Task<TestEntity> CreateTestEntity
        (
            Guid userId,
            Difficulty difficulty = Difficulty.EASY,
            TestType testType = TestType.Mimic
        )
        {
            TestEntity testEntity = new TestEntity()
            {
                UserId = userId,
                Difficulty = difficulty,
                TestType = testType,
                User = await CreateUserService().GetUser(userId)
            };

            return testEntity;
        }
    }
}
