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

        [HttpGet("use-of-the-app")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public IActionResult GetUseOfTheApp([FromQuery] StatsQueryFilterUseOfTheAppDto filtersDto)
        {
            StatsQueryFilterUseOfTheApp filters = _mapper.Map<StatsQueryFilterUseOfTheApp>(filtersDto);
            filters.UserId = GuidOfCurrentUser;

            IEnumerable<int> monthlyUseOfTheAppByUser = _statsService.GetMonthlyUseOfTheAppByUser(filters);

            return Ok(new { stat = monthlyUseOfTheAppByUser });
        }

        [HttpGet("best-streak")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public IActionResult GetBestStreak()
        {
            int bestStreak = _statsService.GetBestStreak(GuidOfCurrentUser);

            return Ok(new { stat = bestStreak });
        }

        [HttpGet("current-streak")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public IActionResult GetCurrentStreak()
        {
            int currentStreak = _statsService.GetCurrentStreak(GuidOfCurrentUser);

            return Ok(new { stat = currentStreak });
        }

        [HttpGet("number-of-learnt-words")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public IActionResult GetNumberOfWordsLearntByUser([FromQuery] StatsQueryFilterNumberOfLearntWordsDto filtersDto)
        {
            StatsQueryFilterNumberOfLearntWords filters = _mapper.Map<StatsQueryFilterNumberOfLearntWords>(filtersDto);
            filters.UserId = GuidOfCurrentUser;

            int numberOfWords = _statsService.GetNumberOfWordsLearntByUser(GuidOfCurrentUser, filters);

            return Ok(new { stat = numberOfWords });
        }

        [HttpGet("percent-learnt")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> GetPercentOfWordsLearntByUser()
        {
            double percent = await _statsService.GetPercentOfWordsLearntByUser(GuidOfCurrentUser);

            return Ok(new { stat = percent });
        }

        [HttpGet("success-rate")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public IActionResult GetSuccessRate([FromQuery] StatsQueryFilterSuccessRateDto filtersDto)
        {
            StatsQueryFilterSuccessRate filters = _mapper.Map<StatsQueryFilterSuccessRate>(filtersDto);
            filters.UserId = GuidOfCurrentUser;

            double rate = _statsService.GetSuccessRate(filters);

            return Ok(new { stat = rate });
        }
    }
}