using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Core.Contracts.Incoming;
using Core.Contracts.OutComing.Tests;
using Core.CustomEntities;
using Core.Entities;
using Core.Entities.Tests;
using Core.Exceptions;
using Core.Interfaces;
using Core.QueryFilters;
using Infraestructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        private readonly IQuestionGeneratorService _questionGeneratorService;
        private readonly IQuestionService _questionService;
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;

        public TestController
        (
            ITestService testService,
            IUserService userService,
            IQuestionGeneratorService questionGeneratorService,
            IQuestionService questionService,
            IUriService uriService,
            IMapper mapper
        )
        {
            _testService = testService;
            _userService = userService;
            _questionGeneratorService = questionGeneratorService;
            _questionService = questionService;
            _uriService = uriService;
            _mapper = mapper;
        }

        [HttpGet("{guid}")]
        [ProducesResponseType(typeof(TestDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Get(Guid guid, [FromQuery] bool populated)
        {
            UserEntity userEntity = await _userService.GetUserByEmail(EmailOfCurrentUser);
            TestEntity test = await GetTest(guid, userEntity.Id);
            IEnumerable<BaseQuestionEntity> questions = await _questionService.GetQuestions(test);

            if (populated)
            {
                PopulatedTestDto testDto = _mapper.Map<PopulatedTestDto>(test);
                testDto.Questions = _mapper.Map<IEnumerable<BasePopulatedQuestionDto>>(questions);

                return Ok(testDto);
            } else
            {
                TestDto testDto = _mapper.Map<TestDto>(test);
                testDto.Questions = _mapper.Map<IEnumerable<BaseQuestionDto>>(questions);

                return Ok(testDto);
            }
        }

        [HttpGet("", Name = nameof(GetAll))]
        [ProducesResponseType(typeof(ICollection<TestDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> GetAll([FromQuery] TestQueryFilterDto filtersDto)
        {
            UserEntity userEntity = await _userService.GetUserByEmail(EmailOfCurrentUser);
            TestQueryFilter filters = _mapper.Map<TestQueryFilter>(filtersDto);
            filters.UserId = userEntity.Id;

            PagedList<TestWithQuestions> pagedListOfTests = await _testService.GetAllTests(filters);
            List<TestDto> testsDto = _mapper.Map<List<TestDto>>(pagedListOfTests);

            Metadata<TestWithQuestions> metadata = new Metadata<TestWithQuestions>(pagedListOfTests);
            metadata = _uriService.UpdateMetadataTests(metadata, filtersDto, Url.RouteUrl(nameof(GetAll)));
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(testsDto);
        }

        [HttpPost("")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Create([FromQuery] TestCreateDto testCreateDto)
        {
            UserEntity userEntity = await _userService.GetUserByEmail(EmailOfCurrentUser);
            TestEntity test = _mapper.Map<TestEntity>(testCreateDto);

            // Create the test
            test.User = userEntity;
            Guid guid = await _testService.AddTest(test);

            // Create the questions
            List<BaseQuestionEntity> questions = new();
            for(int i=0; i<testCreateDto.NumberOfQuestions; ++i)
                questions.Add(_questionGeneratorService.CreateQuestion(test.TestType, test.Difficulty, test.Id));
            
            await _questionService.AddQuestions(test.TestType, questions);

            return CreatedAtAction(nameof(Create), new { guid });
        }

        [HttpDelete("{guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Delete(Guid guid)
        {
            UserEntity userEntity = await _userService.GetUserByEmail(EmailOfCurrentUser);
            TestEntity test = await _testService.GetTest(guid);

            if (!test.UserId.Equals(userEntity.Id))
            {
                throw new ControllerException("You are not authorized to get this test.");
            }

            await _testService.DeleteTest(guid);
            return Ok();
        }

        [HttpDelete("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> DeleteAllTestsFromUser()
        {
            UserEntity userEntity = await _userService.GetUserByEmail(EmailOfCurrentUser);

            await _testService.DeleteAllTestsFromUser(userEntity.Id);
            return Ok();
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