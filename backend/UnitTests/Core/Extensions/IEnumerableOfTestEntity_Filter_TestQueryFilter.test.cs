using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Tests;
using Core.Enums;
using Core.Extensions;
using Core.Interfaces;
using Core.Options;
using Core.QueryFilters;
using Core.Services;
using Microsoft.Extensions.Options;
using Tests.Mocks;
using Xunit;

namespace Tests.Core.Extensions
{
    public partial class TestIEnumerableOfTestEntity_Filter_TestQueryFilter
    {
        [Fact]
        public async Task Extensions_FilterTests_FiltersCorrectly()
        {
            TestService testService = CreateTestService();
            Guid userId = await AddUser();
            TestQueryFilter filters = new TestQueryFilter() { UserId = userId };

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

            IEnumerable<TestEntity> allTests = await _unitOfWork.TestRepository.GetAll();
            Assert.Empty(allTests);

            // Filter by user
            foreach (TestEntity test in testEntities)
            {
                await testService.AddTest(test);
            }

            IQueryable<TestEntity> allTestsQuery = _unitOfWork.TestRepository.GetAllAsQueryable();
            allTests = allTestsQuery.Filter(filters);
            Assert.Equal(testEntities, allTests);

            // Filter by type
            filters.TestType = TestType.Mimic;
            IEnumerable<TestEntity> mimicTests = testEntities
                .Where(test => test.TestType == filters.TestType)
                .ToList();

            allTestsQuery = _unitOfWork.TestRepository.GetAllAsQueryable();
            allTests = allTestsQuery.Filter(filters);
            Assert.Equal(allTests, mimicTests);
            filters.TestType = null;

            // Filter by difficulty
            filters.Difficulty = Difficulty.EASY;
            IEnumerable<TestEntity> easyTests = testEntities
                .Where(test => test.Difficulty == Difficulty.EASY)
                .ToList();

            allTestsQuery = _unitOfWork.TestRepository.GetAllAsQueryable();
            allTests = allTestsQuery.Filter(filters);
            Assert.Equal(allTests, easyTests);
        }
    }

    public partial class TestIEnumerableOfTestEntity_Filter_TestQueryFilter
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public TestIEnumerableOfTestEntity_Filter_TestQueryFilter()
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

        private async Task<Guid> AddUser()
        {
            UserService userService = CreateUserService();
            UserEntity userEntity = new UserEntity("test@mail.com", "test");

            return await userService.AddUser(userEntity);
        }

        private UserService CreateUserService()
        {
            return new UserService(_unitOfWork);
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
