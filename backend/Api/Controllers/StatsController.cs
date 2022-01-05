using Core.Interfaces;
using Infrastructure.Interfaces;
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

        public StatsController
        (
          IStatsService statsService
        )
        {
          _statsService = statsService;
        }

    
    }
}