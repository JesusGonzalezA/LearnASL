using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Core.Contracts.Incoming;
using Core.Interfaces;
using Core.QueryFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/stats")]
    [Authorize]
    [ApiController]
    public class StatsController : BaseController
    {
        private readonly IStatsService _statsService;
        private readonly IMapper _mapper;

        public StatsController
        (
            IStatsService statsService,
            IMapper mapper
        )
        {
            _statsService = statsService;
            _mapper = mapper;
        }

        [HttpGet("/use-of-the-app")]
        [ProducesResponseType(typeof(IEnumerable<int>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public IActionResult GetUseOfTheApp([FromQuery] StatsQueryFilterUseOfTheAppDto filtersDto)
        {
            StatsQueryFilterUseOfTheApp filters = _mapper.Map<StatsQueryFilterUseOfTheApp>(filtersDto);
            filters.UserId = GuidOfCurrentUser;

            IEnumerable<int> monthlyUseOfTheAppByUser = _statsService.GetMonthlyUseOfTheAppByUser(filters);

            return Ok(monthlyUseOfTheAppByUser);
        }

        [HttpGet("/best-streak")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public IActionResult GetBestStreak()
        {
            int bestStreak = _statsService.GetBestStreak(GuidOfCurrentUser);

            return Ok(bestStreak);
        }

        [HttpGet("/current-streak")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public IActionResult GetCurrentStreak()
        {
            int currentStreak = _statsService.GetCurrentStreak(GuidOfCurrentUser);

            return Ok(currentStreak);
        }

        [HttpGet("/number-of-learnt-words")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public IActionResult GetNumberOfWordsLearntByUser([FromQuery] StatsQueryFilterNumberOfLearntWordsDto filtersDto)
        {
            StatsQueryFilterNumberOfLearntWords filters = _mapper.Map<StatsQueryFilterNumberOfLearntWords>(filtersDto);
            filters.UserId = GuidOfCurrentUser;

            int numberOfWords = _statsService.GetNumberOfWordsLearntByUser(GuidOfCurrentUser, filters);

            return Ok(numberOfWords);
        }

        [HttpGet("/percent-learnt")]
        [ProducesResponseType(typeof(double), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> GetPercentOfWordsLearntByUser()
        {
            double percent = await _statsService.GetPercentOfWordsLearntByUser(GuidOfCurrentUser);

            return Ok(percent);
        }

        [HttpGet("/success-rate")]
        [ProducesResponseType(typeof(double), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> GetSuccessRate([FromQuery] StatsQueryFilterSuccessRateDto filtersDto)
        {
            StatsQueryFilterSuccessRate filters = _mapper.Map<StatsQueryFilterSuccessRate>(filtersDto);
            filters.UserId = GuidOfCurrentUser;

            double rate = await _statsService.GetSuccessRate(GuidOfCurrentUser);

            return Ok(rate);
        }
    }
}