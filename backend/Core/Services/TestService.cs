using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Tests;
using Core.Exceptions;
using Core.Interfaces;

namespace Core.Services
{
    public class TestService : ITestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> AddTest(TestOptionWordToVideoEntity test)
        {
            Guid guid = await _unitOfWork.TestOptionWordToVideoRepository.Add(test);

            foreach(IQuestion question in test.Questions)
            {
                question.TestId = guid;
            }
            await _unitOfWork.TestOptionWordToVideoRepository.Update(test);

            return guid;
        }

        public async Task DeleteTest(Guid guid)
        {
            TestOptionWordToVideoEntity test = await _unitOfWork.TestOptionWordToVideoRepository.GetById(guid);

            if (test == null)
            {
                throw new BusinessException("Test does not exist");
            }

            await _unitOfWork.TestOptionWordToVideoRepository.Delete(guid);
        }

        public async Task<TestOptionWordToVideoEntity> GetTest(Guid id)
        {
            TestOptionWordToVideoEntity test =  await _unitOfWork.TestOptionWordToVideoRepository.GetById(id);

            if ( test == null )
            {
                throw new BusinessException("Test does not exist");
            }

            return test;
        }

    }
}
