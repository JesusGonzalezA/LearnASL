using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Core.Contracts.Incoming;
using Core.Contracts.OutComing.Tests;
using Core.Entities;
using Core.Entities.Tests;
using Core.Enums;
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
        private readonly IMapper _mapper;
        private readonly ITestGenerator _testGenerator;

        public TestController
        (
            ITestService testService,
            IUserService userService,
            IMapper mapper,
            ITestGenerator testGenerator
        )
        {
            _testService = testService;
            _userService = userService;
            _mapper = mapper;
            _testGenerator = testGenerator;
        }

        [HttpGet("{testType}/{guid}")]
        [ProducesResponseType(typeof(ITest), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Get(TestType testType, Guid guid)
        {
            ITest test = await _testService.GetTest(testType, guid);

            if (!test.User.Email.Equals(EmailOfCurrentUser))
            {
                throw new ControllerException("You are not authorized to get this test.");
            }

            ITestDto testDto = _mapper.Map<ITestDto>(test);
            return Ok(testDto);
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(ICollection<ITest>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> GetAll()
        {
            UserEntity userEntity = await _userService.GetUserByEmail(EmailOfCurrentUser);
            IEnumerable<ITest> tests = await _testService.GetAllTests(userEntity.Id);

            IEnumerable<ISimpleTestDto> testsDto = _mapper.Map<IEnumerable<ISimpleTestDto> >(tests);
            return Ok(testsDto);
        }

        [HttpPost("{testType}")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Create(TestType testType, [FromBody] TestCreateDto testDto)
        {
            UserEntity userEntity = await _userService.GetUserByEmail(EmailOfCurrentUser);

            // Create test
            ITest test = _testGenerator.CreateTest
            (
                testType,
                testDto.Difficulty,
                testDto.NumberOfQuestions
            );
            test.User = userEntity;
            Guid guid = await _testService.AddTest(testType, test);

            return CreatedAtAction(nameof(Create), new { guid });
        }

        [HttpDelete("{testType}/{guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Delete(TestType testType, Guid guid)
        {
            ITest test = await _testService.GetTest(testType, guid);

            if (!test.User.Email.Equals(EmailOfCurrentUser))
            {
                throw new ControllerException("You are not authorized to delete this test.");
            }

            await _testService.DeleteTest(testType, guid);
            return Ok();
        }
    }
}
