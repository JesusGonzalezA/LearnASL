using System;
using System.Net;
using System.Threading.Tasks;
using Core.Contracts.Incoming;
using Core.Entities;
using Core.Interfaces;
using Infraestructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    [Authorize]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Get()
        {
            UserEntity userEntity = await _userService.GetUserByEmail(EmailOfCurrentUser);
            return Ok(userEntity);
        }
    }
}
