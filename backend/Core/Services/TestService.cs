using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Tests;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces;
using System.Linq;

namespace Core.Services
{
    public class TestService : ITestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public async Task<IEnumerable<TestEntity> > GetAllTests(Guid userGuid)
        {
            IEnumerable<TestEntity> AllTests = await _unitOfWork.TestRepository.GetAll();

            IEnumerable<TestEntity> testsFromUser = AllTests.Where(test => test.UserId == userGuid);

            return testsFromUser;
        }
    }
}