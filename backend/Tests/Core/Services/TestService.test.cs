using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomEntities;
using Core.Entities;
using Core.Entities.Tests;
using Core.Enums;
using Core.Exceptions;
using Core.Extensions;
using Core.Interfaces;
using Core.Options;
using Core.QueryFilters;
using Core.Services;
using Microsoft.Extensions.Options;
using Tests.Mocks;
using Xunit;

namespace Tests.Core.Services
{
    public partial class TestTestService
    {
        [Fact]
        public async Task TestService_GetTest()
        {
            TestService testService = CreateTestService();
            Guid userId = await AddUser();
            TestEntity testEntity = await CreateTestEntity(userId);

            TestQueryFilter filters = new TestQueryFilter() { UserId = userId };
            PagedList<TestWithQuestions> allTests = testService.GetAllTestsPaged(filters);
            Assert.Empty(allTests);

            Guid testGuid = await testService.AddTest(testEntity);
            Assert.NotNull(await testService.GetTest(testGuid));
        }

        [Fact]
        public async Task TestService_AddTest_ReturnsGuid()
        {
            TestService testService = CreateTestService();
            Guid userId = await AddUser();
            TestEntity testEntity = await CreateTestEntity(userId);

            await testService.AddTest(testEntity);
        }

        [Fact]
        public async Task TestService_DeleteTest_DeletesTest()
        {
            TestService testService = CreateTestService();
            Guid userId = await AddUser();
            TestEntity testEntity = await CreateTestEntity(userId);

            Guid guid = await testService.AddTest(testEntity);
            await testService.DeleteTest(guid);
        }

        [Fact]
        public async Task TestService_DeleteTest_ThrowsBETestDoesNotExist()
        {
            TestService testService = CreateTestService();
            Guid userId = await AddUser();
            TestEntity testEntity = await CreateTestEntity(userId);

            Guid guid = await testService.AddTest(testEntity);
            await testService.DeleteTest(guid);

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await testService.DeleteTest(guid);
            });
        }

        [Fact]
        public async Task TestService_DeleteAllTestsFromUser_DeletesAllTests()
        {
            TestService testService = CreateTestService();
            Guid userId = await AddUser();

            await testService.AddTest(await CreateTestEntity(userId));
            await testService.AddTest(await CreateTestEntity(userId));

            TestQueryFilter filters = new TestQueryFilter() { UserId = userId };
            PagedList<TestWithQuestions> allTests = testService.GetAllTestsPaged(filters);

            Assert.NotEmpty(allTests);

            await testService.DeleteAllTestsFromUser(userId);
            allTests = testService.GetAllTestsPaged(filters);

            Assert.Empty(allTests);
        }

        [Fact]
        public async Task TestService_GetAll_GetsAll()
        {
            // Create tests
            TestService testService = CreateTestService();
            Guid userId = await AddUser();

            IEnumerable<TestEntity> testEntities = new List<TestEntity>()
            {
                await CreateTestEntity(userId, Difficulty.EASY, TestType.Mimic),
                await CreateTestEntity(userId, Difficulty.MEDIUM, TestType.Mimic_Error),
                await CreateTestEntity(userId, Difficulty.HARD, TestType.QA),
                await CreateTestEntity(userId, Difficulty.EASY, TestType.QA_Error),
                await CreateTestEntity(userId, Difficulty.MEDIUM, TestType.OptionVideoToWord),
                await CreateTestEntity(userId, Difficulty.HARD, TestType.OptionVideoToWord_Error),
                await CreateTestEntity(userId, Difficulty.EASY, TestType.OptionWordToVideo),
                await CreateTestEntity(userId, Difficulty.MEDIUM, TestType.OptionWordToVideo_Error)
            };

            // Add tests
            foreach (TestEntity test in testEntities)
            {
                await testService.AddTest(test);
            }

            // Get tests
            TestQueryFilter filters = new TestQueryFilter() { UserId = userId };
            IQueryable<TestEntity> allTests = _unitOfWork.TestRepository.GetAllAsQueryable();
            allTests = allTests.Filter(filters);

            PagedList<TestEntity> allTestsPaged = PagedList<TestEntity>.Create(allTests, _paginationOptions.DefaultPageNumber, _paginationOptions.DefaultPageSize);
            PagedList<TestWithQuestions> allTestsPagedWithQuestions = testService.GetAllTestsPaged(filters);

            Assert.Equal(allTestsPaged.TotalCount, allTestsPagedWithQuestions.TotalCount);

        }
    }

    public partial class TestTestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public TestTestService()
        {
            _unitOfWork = MockUnitOfWork.GetMockUnitOfWork();
            _paginationOptions = new PaginationOptions()
            {
                DefaultPageNumber = 1,
                DefaultPageSize = 10,
                MaximumPageSize = 20
            };
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
