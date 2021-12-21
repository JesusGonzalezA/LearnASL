using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Core.Contracts.Incoming;
using Core.Contracts.OutComing;
using Core.Entities;
using Core.Entities.Tests;
using Core.Exceptions;
using Core.Interfaces;
using Infraestructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/test")]
    [Authorize]
    [ApiController]
    public class TestController : BaseController
    {
        private readonly ITestService _testService;
        private readonly IUserService _userService;
        private readonly IQuestionsService _questionsService;
        private readonly IMapper _mapper;

        public TestController
        (
            ITestService testService,
            IUserService userService,
            IQuestionsService questionsService,
            IMapper mapper
        )
        {
            _testService = testService;
            _userService = userService;
            _questionsService = questionsService;
            _mapper = mapper;
        }

        [HttpGet("{guid}")]
        [ProducesResponseType(typeof(TestOptionWordToVideoEntity), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Get(Guid guid)
        {
            TestOptionWordToVideoEntity test = await _testService.GetTest(guid);

            if (!test.User.Email.Equals(EmailOfCurrentUser))
            {
                throw new ControllerException("You are not authorized to get this test.");
            }

            TestOptionWordToVideoDto testDto = _mapper.Map<TestOptionWordToVideoDto>(test);

            return Ok(testDto);
        }

        [HttpPost()]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Create([FromBody] TestCreateDto testDto)
        {
            TestOptionWordToVideoEntity test = _mapper.Map<TestOptionWordToVideoEntity>(testDto);
            UserEntity userEntity = await _userService.GetUserByEmail(EmailOfCurrentUser);
            test.User = userEntity;

            Guid guid = await _testService.AddTest(test);

            await _testService.AddQuestions
            (
                guid,
                _questionsService.GenerateQuestionsOptionWordToVideoEntity
                (
                    guid,
                    testDto.NumberOfQuestions,
                    testDto.IsErrorTest
                )
            );

            return CreatedAtAction(nameof(Create), new { guid });
        }

        [HttpDelete("{guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Delete(Guid guid)
        {
            TestOptionWordToVideoEntity test = await _testService.GetTest(guid);

            if (!test.User.Email.Equals(EmailOfCurrentUser))
            {
                throw new ControllerException("You are not authorized to delete this test.");
            }

            await _testService.DeleteTest(guid);
            return Ok();
        }
    }
}
