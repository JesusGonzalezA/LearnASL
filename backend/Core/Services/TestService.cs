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

        public async Task<Guid> AddTest(TestType testType, ITest test)
        {
            Guid guid = await AddTestToRepository(testType, test);

            foreach(IQuestion question in test.Questions)
            {
                question.TestId = guid;
            }

            await UpdateTestInRepository(testType, test);

            return guid;
        }

        public async Task DeleteTest(TestType testType, Guid guid)
        {
            ITest test = await GetTestFromRepository(testType, guid);

            if (test == null)
            {
                throw new BusinessException("Test does not exist");
            }

            await DeleteTestFromRepository(testType, guid);
        }

        public async Task<ITest> GetTest(TestType testType, Guid guid)
        {
            ITest test = await GetTestFromRepository(testType, guid);

            if ( test == null )
            {
                throw new BusinessException("Test does not exist");
            }

            return test;
        }

        private async Task<Guid> AddTestToRepository(TestType testType, ITest test)
        {
            Guid guid;

            switch (testType)
            {
                case TestType.OptionWordToVideoEntity:
                case TestType.OptionWordToVideoEntity_Error:
                    guid = await _unitOfWork.TestOptionWordToVideoRepository.Add((TestOptionWordToVideoEntity)test);
                    break;

                case TestType.OptionVideoToWordEntity:
                case TestType.OptionVideoToWordEntity_Error:
                    guid = await _unitOfWork.TestOptionVideoToWordRepository.Add((TestOptionVideoToWordEntity)test);
                    break;

                default:
                    throw new BusinessException("Invalid test type");
            }

            return guid;
        }

        private async Task UpdateTestInRepository(TestType testType, ITest test)
        {
            switch (testType)
            {
                case TestType.OptionWordToVideoEntity:
                case TestType.OptionWordToVideoEntity_Error:
                    await _unitOfWork.TestOptionWordToVideoRepository.Update((TestOptionWordToVideoEntity)test);
                    break;

                case TestType.OptionVideoToWordEntity:
                case TestType.OptionVideoToWordEntity_Error:
                    await _unitOfWork.TestOptionVideoToWordRepository.Update((TestOptionVideoToWordEntity)test);
                    break;

                default:
                    throw new BusinessException("Invalid test type");
            }
        }

        private async Task DeleteTestFromRepository(TestType testType, Guid guid)
        {
            switch (testType)
            {
                case TestType.OptionWordToVideoEntity:
                case TestType.OptionWordToVideoEntity_Error:
                    await _unitOfWork.TestOptionWordToVideoRepository.Delete(guid);
                    break;

                case TestType.OptionVideoToWordEntity:
                case TestType.OptionVideoToWordEntity_Error:
                    await _unitOfWork.TestOptionVideoToWordRepository.Delete(guid);
                    break;

                default:
                    throw new BusinessException("Invalid test type");
            }
        }

        private async Task<ITest> GetTestFromRepository(TestType testType, Guid guid)
        {
            ITest test;

            switch (testType)
            {
                case TestType.OptionWordToVideoEntity:
                case TestType.OptionWordToVideoEntity_Error:
                    test = await _unitOfWork.TestOptionWordToVideoRepository.GetById(guid);
                    break;

                case TestType.OptionVideoToWordEntity:
                case TestType.OptionVideoToWordEntity_Error:
                    test = await _unitOfWork.TestOptionVideoToWordRepository.GetById(guid);
                    break;

                default:
                    throw new BusinessException("Invalid test type");
            }

            return test;
        }

        public async Task<IEnumerable<ITest> > GetAllTests(Guid userGuid)
        {
            IEnumerable<ITest> testsOptionWordToVideo
                = await _unitOfWork.TestOptionWordToVideoRepository.GetAll();
            IEnumerable<ITest> testsOptionVideoToWord
                = await _unitOfWork.TestOptionVideoToWordRepository.GetAll();

            IEnumerable<ITest> allTests = testsOptionVideoToWord
                .Concat(testsOptionWordToVideo)
                .Where(test => test.UserId == userGuid);

            return allTests;
        }
    }
}
