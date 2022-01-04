using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Core.Contracts.Incoming;
using Core.CustomEntities;
using Core.Entities;
using Core.Entities.Tests;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces;
using Core.Options;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/question")]
    [Authorize]
    [ApiController]
    public class QuestionController : BaseController
    {
        private readonly ITestService _testService;
        private readonly IUserService _userService;
        private readonly IQuestionService _questionService;
        private readonly IUriService _uriService;
        private readonly IStoreService _storeService;

        public QuestionController
        (
            ITestService testService,
            IUserService userService,
            IQuestionService questionService,
            IUriService uriService,
            IStoreService storeService
        )
        {
            _testService = testService;
            _userService = userService;
            _questionService = questionService;
            _uriService = uriService;
            _storeService = storeService;
        }

        [HttpPut("{guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Reply(Guid guid, [FromQuery] QuestionReplyDto questionReplyDto)
        {
            BaseQuestionEntity question = await _questionService.GetQuestion(questionReplyDto.TestType, guid);
            TestEntity test = await GetTest(question.TestId, GuidOfCurrentUser);

            ValidateQuestionReplyDto(test.TestType, questionReplyDto);
            string filename = await SaveQuestionVideoIfNecessary(test.TestType, GuidOfCurrentUser, test.Id, guid, questionReplyDto);
            string videoUri = _uriService.GetVideoUri(filename);

            UpdateQuestionParameters updateQuestionParameters = new UpdateQuestionParameters()
            {
                VideoUser = videoUri,
                UserAnswer = questionReplyDto.UserAnswer
            };

            await _questionService.UpdateQuestion(test.TestType, guid, updateQuestionParameters);

            return Ok();
        }

        private async Task<string> SaveQuestionVideoIfNecessary
        (
            TestType testType,
            Guid userId,
            Guid testGuid,
            Guid questionGuid,
            QuestionReplyDto questionReplyDto
        )
        {
            if (testType != TestType.Mimic_Error
               && testType != TestType.Mimic
               && testType != TestType.QA_Error
               && testType != TestType.QA)
            {
                return null;
            }

            string extension = Path.GetExtension(questionReplyDto.VideoUser.FileName);
            string filename = $"{userId}/{testGuid}/{questionGuid}{extension}";
            
            await _storeService.SaveVideo(filename, questionReplyDto.VideoUser);

            return filename;
        }

        private static void ValidateQuestionReplyDto(TestType testType, QuestionReplyDto questionReplyDto)
        {
            switch(testType)
            {
                case TestType.OptionWordToVideo_Error:
                case TestType.OptionWordToVideo:
                case TestType.OptionVideoToWord_Error:
                case TestType.OptionVideoToWord:
                    if (questionReplyDto.UserAnswer == null)
                        throw new ControllerException("Invalid reply. Please provide your answer (option).");
                    break;

                case TestType.Mimic_Error:
                case TestType.Mimic:
                case TestType.QA_Error:
                case TestType.QA:
                    if (questionReplyDto.VideoUser == null)
                        throw new ControllerException("Invalid reply. Please provide your answer (video).");
                    break;
            }
        }

        private async Task<TestEntity> GetTest(Guid testId, Guid userId)
        {
            TestEntity test = await _testService.GetTest(testId);

            if (!test.UserId.Equals(userId))
            {
                throw new ControllerException("You are not authorized to get this test.");
            }

            return test;
        }
    }
}