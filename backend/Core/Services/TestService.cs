using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Tests;
using Core.Exceptions;
using Core.Interfaces;
using System.Linq;
using Core.CustomEntities;
using Microsoft.Extensions.Options;
using Core.QueryFilters;

namespace Core.Services
{
    public class TestService : ITestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public TestService
        (
            IUnitOfWork unitOfWork,
            IOptions<PaginationOptions> paginationOptions
        )
        {
            _unitOfWork = unitOfWork;
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

        public async Task<TestEntity> GetTest(Guid guid)
        {
            TestEntity test = await _unitOfWork.TestRepository.GetById(guid);

            if (test == null)
            {
                throw new BusinessException("Test does not exist");
            }

            return test;
        }

        public async Task<PagedList<TestEntity> > GetAllTests(TestQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            IEnumerable<TestEntity> tests = await _unitOfWork.TestRepository.GetAll();

            tests = tests.Where(test => test.UserId == filters.UserId);

            if (filters.TestType != null)
            {
                tests = tests.Where(test => test.TestType == filters.TestType);
            }

            if (filters.Difficulty != null)
            {
                tests = tests.Where(test => test.Difficulty == filters.Difficulty);
            }

            PagedList<TestEntity> pagedTests = PagedList<TestEntity>.Create
            (
                tests,
                filters.PageNumber,
                filters.PageSize
            );

            return pagedTests;
        }
    }
}