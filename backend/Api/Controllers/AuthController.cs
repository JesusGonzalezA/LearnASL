using Microsoft.AspNetCore.Mvc;
using Infraestructure.Interfaces;
using Core.Interfaces;
using System.Threading.Tasks;
using Core.Contracts.Incoming;
using System.Net;
using Core.Entities;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IPasswordService _passwordService;

        public AuthController
        (
            IUserService userService,
            ITokenService tokenService,
            IEmailService emailService,
            IPasswordService passwordService
        )
        {
            _userService = userService;
            _tokenService = tokenService;
            _emailService = emailService;
            _passwordService = passwordService;
        }

        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            UserEntity user = await _userService.GetUserByEmail(login.Email);

            if (user == null)
            {
                return Conflict("User does not exist.");
            }

            bool match = _passwordService.Check(user.Password, login.Password);
            bool isConfirmed = await _userService.CheckConfirmedUser(login.Email);

            if (!isConfirmed)
            {
                return Conflict("Email is not confirmed.");
            }
            if (!match)
            {
                return Conflict("Credentials are not correct.");
            }

            return Ok(_tokenService.GenerateJWTToken(login.Email));
        }

        [HttpPost("register")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Register([FromBody] UserEntity userEntity)
        {
            userEntity.Password = _passwordService.Hash(userEntity.Password);
            Guid guid = await _userService.AddUser(userEntity);
            await _emailService.SendEmailConfirmationEmail(userEntity.Email, userEntity.TokenEmailConfirmation);
            return CreatedAtAction(nameof(Register), new { guid });
        }

        [HttpDelete("delete")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Delete()
        {
            await _userService.DeleteUser(EmailOfCurrentUser);
            return Ok();
        }

        [HttpPut("password-recovery")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> PasswordRecovery([FromHeader] string token, [FromBody] LoginDto login)
        {
            string hashedPassword = _passwordService.Hash(login.Password);
            await _userService.ChangePassword(login.Email, hashedPassword, token);
            return Ok();
        }

        [HttpPut("email-confirmation")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> EmailConfirmation([FromHeader] string token, string email)
        {
            await _userService.ConfirmEmail(email, token);
            return Ok();
        }

        [HttpPut("password-recovery/start")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> StartPasswordRecovery(string email)
        {
            string token = _tokenService.GenerateJWTToken();
            await _userService.UpdateTokenPasswordRecovery(email, token);
            await _emailService.SendPasswordRecoveryEmail(email, token);
            return Ok();
        }

        [HttpPut("email-confirmation/start")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> StartEmailConfirmation(string email)
        {
            string token = _tokenService.GenerateJWTToken();
            await _userService.UpdateTokenEmailConfirmation(email, token);
            await _emailService.SendEmailConfirmationEmail(email, token);
            return Ok();
        }

    }
}