using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Tests;
using Core.Exceptions;
using Core.Interfaces;
using Core.Options;
using Microsoft.Extensions.Options;
using Core.QueryFilters;
using Core.Extensions;
using Core.CustomEntities;
using System.Linq;

namespace Core.Services
{
    public class TestService : ITestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQuestionService _questionService;
        private readonly PaginationOptions _paginationOptions;

        public TestService
        (
            IUnitOfWork unitOfWork,
            IQuestionService questionService,
            IOptions<PaginationOptions> paginationOptions
        )
        {
            _unitOfWork = unitOfWork;
            _questionService = questionService;
            _paginationOptions = paginationOptions.Value;
        }

        public async Task<Guid> AddTest(TestEntity test)
        {
            Guid guid = await _unitOfWork.TestRepository.Add(test);
            return guid;
        }

        public async Task DeleteTest(Guid guid)
        {
            TestEntity test = await _unitOfWork.TestRepository.GetById(guid);

            if (test == null)
            {
                throw new BusinessException("Test does not exist");
            }

            await _unitOfWork.TestRepository.Delete(guid);
        }

        public async Task DeleteAllTestsFromUser(Guid userId)
        {
            await _unitOfWork.TestRepository.DeleteAllTestsFromUser(userId);
        }

        public async Task<TestEntity> GetTest(Guid guid)
        {
            TestEntity test = await _unitOfWork.TestRepository.GetById(guid);

            if (test == null)
            {
                throw new BusinessException("Test does not exist");
            }

            return test;
        }

        public PagedList<TestWithQuestions> GetAllTestsPaged(TestQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            IList<TestWithQuestions> testsWithQuestions = GetAllTests(filters);
            
            PagedList<TestWithQuestions> pagedTests = PagedList<TestWithQuestions>.Create
            (
                testsWithQuestions,
                filters.PageNumber,
                filters.PageSize
            );

            return pagedTests;
        }

        public IList<TestWithQuestions> GetAllTests(TestQueryFilter filters)
        {
            IQueryable<TestEntity> tests = _unitOfWork.TestRepository.GetAllAsQueryable();

            IEnumerable<TestEntity> filteredTests = tests.Filter(filters).ToList();
            IList<TestWithQuestions> testsWithQuestions = PopulateTestsWithQuestions(filteredTests);

            return testsWithQuestions;
        }

        private IList<TestWithQuestions> PopulateTestsWithQuestions(IEnumerable<TestEntity> tests)
        {
            IList<TestWithQuestions> testsWithQuestions = new List<TestWithQuestions>();

            foreach (TestEntity test in tests)
            {
                IEnumerable<BaseQuestionEntity> questions = _questionService.GetQuestions(test);
                TestWithQuestions testWithQuestions = new TestWithQuestions()
                {
                    Id = test.Id,
                    CreatedOn = test.CreatedOn,
                    ModifiedOn = test.ModifiedOn,
                    UserId = test.UserId,
                    User = test.User,
                    Difficulty = test.Difficulty,
                    TestType = test.TestType,
                    Questions = questions
                };

                testsWithQuestions.Add(testWithQuestions);
            }

            return testsWithQuestions;
        }
    }
}