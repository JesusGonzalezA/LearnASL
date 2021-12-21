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

        public async Task AddQuestions(Guid guid, ICollection<QuestionOptionWordToVideoEntity> questions)
        {
            TestOptionWordToVideoEntity test = await _unitOfWork.TestOptionWordToVideoRepository.GetById(guid);

            if (test == null)
            {
                throw new BusinessException("Test does not exist");
            }

            test.Questions = questions;
            await _unitOfWork.TestOptionWordToVideoRepository.Update(test);
        }

        public async Task<Guid> AddTest(TestOptionWordToVideoEntity test)
        {
            return await _unitOfWork.TestOptionWordToVideoRepository.Add(test);
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
