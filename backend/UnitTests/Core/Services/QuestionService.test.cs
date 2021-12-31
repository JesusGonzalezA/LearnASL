using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomEntities;
using Core.Entities;
using Core.Entities.Tests;
using Core.Enums;
using Core.Exceptions;
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
            // Add test
            QuestionService questionService = CreateQuestionService();
            TestService testService = CreateTestService();

            Guid userId = await AddUser();
            TestEntity testEntity = await CreateTestEntity(userId, Difficulty.EASY, testType);

            Guid testId = await testService.AddTest(testEntity);
            TestEntity testEntityDB = await testService.GetTest(testId);

            Assert.Empty(await questionService.GetQuestions(testEntityDB));

            // Add questions
            IList<BaseQuestionEntity> questions = await _questionGeneratorService.CreateQuestions(1, testType, Difficulty.EASY, testId);
            await questionService.AddQuestions(testType, questions);

            Assert.NotEmpty(await questionService.GetQuestions(testEntityDB));
        }

        [Theory]
        [MemberData(nameof(AllTestTypes))]
        public async Task QuestionService_GetQuestions_GetsCorrectly(TestType testType)
        {
            // Add test
            QuestionService questionService = CreateQuestionService();
            TestService testService = CreateTestService();

            Guid userId = await AddUser();
            TestEntity testEntity = await CreateTestEntity(userId, Difficulty.EASY, testType);

            Guid testId = await testService.AddTest(testEntity);
            TestEntity testEntityDB = await testService.GetTest(testId);

            Assert.Empty(await questionService.GetQuestions(testEntityDB));

            // Add questions
            IList<BaseQuestionEntity> questions = await _questionGeneratorService.CreateQuestions(1, testType, Difficulty.EASY, testId);
            await questionService.AddQuestions(testType, questions);

            IEnumerable<BaseQuestionEntity> allQuestions = await questionService.GetQuestions(testEntityDB);
            Assert.Contains(questions.ElementAt(0), allQuestions);
        }

        [Theory]
        [MemberData(nameof(AllTestTypes))]
        public async Task QuestionService_UpdateQuestion_DoesNotThrowException(TestType testType)
        {
            // Add test
            QuestionService questionService = CreateQuestionService();
            TestService testService = CreateTestService();

            Guid userId = await AddUser();
            TestEntity testEntity = await CreateTestEntity(userId, Difficulty.EASY, testType);

            Guid testId = await testService.AddTest(testEntity);
            TestEntity testEntityDB = await testService.GetTest(testId);

            Assert.Empty(await questionService.GetQuestions(testEntityDB));

            // Add questions
            IList<BaseQuestionEntity> questions = await _questionGeneratorService.CreateQuestions(1, testType, Difficulty.EASY, testId);
            await questionService.AddQuestions(testType, questions);

            IEnumerable<BaseQuestionEntity> allQuestions = await questionService.GetQuestions(testEntityDB);
            BaseQuestionEntity questionDB = allQuestions.ToList().ElementAt(0);

            // Update
            UpdateQuestionParameters parameters = new UpdateQuestionParameters()
            {
                VideoUser = "video",
                UserAnswer = "answer"
            };

            await questionService.UpdateQuestion(testType, questionDB.Id, parameters);
        }

        [Theory]
        [MemberData(nameof(AllTestTypes))]
        public async Task QuestionService_GetQuestion_GetsTheQuestion(TestType testType)
        {
            // Add test
            QuestionService questionService = CreateQuestionService();
            TestService testService = CreateTestService();

            Guid userId = await AddUser();
            TestEntity testEntity = await CreateTestEntity(userId, Difficulty.EASY, testType);

            Guid testId = await testService.AddTest(testEntity);
            TestEntity testEntityDB = await testService.GetTest(testId);

            Assert.Empty(await questionService.GetQuestions(testEntityDB));

            // Add questions
            IList<BaseQuestionEntity> questions = await _questionGeneratorService.CreateQuestions(1, testType, Difficulty.EASY, testId);
            await questionService.AddQuestions(testType, questions);

            IEnumerable<BaseQuestionEntity> allQuestions = await questionService.GetQuestions(testEntityDB);
            BaseQuestionEntity questionDB = allQuestions.ToList().ElementAt(0);

            Assert.Equal(questionDB, await questionService.GetQuestion(testType, questionDB.Id));
        }

        [Theory]
        [MemberData(nameof(AllTestTypes))]
        public async Task QuestionService_GetQuestion_ThrowsBEQuestionDoesNotExist(TestType testType)
        {
            QuestionService questionService = CreateQuestionService();

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await questionService.GetQuestion(testType, Guid.Empty);
            });
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
            _questionGeneratorService = new QuestionGeneratorService(_unitOfWork);
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

        private QuestionService CreateQuestionService()
        {
            return new QuestionService(_unitOfWork);
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
