using System;
using System.Net;
using System.Threading.Tasks;
using Core.Contracts.Incoming;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Post([FromBody] UserEntity userEntity)
        {
            Guid guid = await _userService.AddUser(userEntity);
            return CreatedAtAction(nameof(Post), new { guid });
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Delete()
        {
            await _userService.DeleteUser(EmailOfCurrentUser);
            return Ok();
        }

        [HttpPut("password")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> ChangePassword([FromHeader] string token, [FromBody] LoginDto login)
        {
            await _userService.ChangePassword(login.Email, login.Password, token);
            return Ok();
        }

        [HttpPut("confirmation")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> ConfirmEmail([FromHeader] string token, string email)
        {
            await _userService.ConfirmEmail(email, token);
            return Ok();
        }

        [HttpPut("token/passwordRecovery")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> RegenerateTokenPasswordRecovery(string email)
        {
            string token = await _userService.RegenerateTokenPasswordRecovery(email);
            return Ok(new { token });
        }

        [HttpPut("token/emailConfirmation")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> RegenerateTokenEmailConfirmation(string email)
        {
            string token = await _userService.RegenerateTokenEmailConfirmation(email);
            return Ok(new { token });
        }
    }
}
