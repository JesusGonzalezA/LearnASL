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
        public async Task<IActionResult> GetUseOfTheApp([FromQuery] StatsQueryFilterUseOfTheAppDto filtersDto)
        {
            StatsQueryFilterUseOfTheApp filters = _mapper.Map<StatsQueryFilterUseOfTheApp>(filtersDto);
            filters.UserId = GuidOfCurrentUser;

            IEnumerable<int> monthlyUseOfTheAppByUser = _statsService.GetMonthlyUseOfTheAppByUser(filters);

            return Ok(monthlyUseOfTheAppByUser);
        }
    }
}