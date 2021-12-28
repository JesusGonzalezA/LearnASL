using Microsoft.AspNetCore.Mvc;
using Infraestructure.Interfaces;
using Core.Interfaces;
using System.Threading.Tasks;
using Core.Contracts.Incoming;
using System.Net;
using Core.Entities;
using System;
using Microsoft.AspNetCore.Authorization;
using Api.Responses;
using Core.Exceptions;
using System.Collections.Generic;

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
        private readonly IStoreService _storeService;

        public AuthController
        (
            IUserService userService,
            ITokenService tokenService,
            IEmailService emailService,
            IPasswordService passwordService,
            IStoreService storeService
        )
        {
            _userService = userService;
            _tokenService = tokenService;
            _emailService = emailService;
            _passwordService = passwordService;
            _storeService = storeService;
        }

        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            try
            {
                UserEntity user = await _userService.GetUserByEmail(login.Email);
                if (user == null)
                {
                    throw new ControllerException("User does not exist.");
                }
                bool match = _passwordService.Check(user.Password, login.Password);
                bool isConfirmed = await _userService.CheckConfirmedUser(login.Email);

                if (!isConfirmed)
                {
                    throw new ControllerException("Email is not confirmed.");
                }
                if (!match)
                {
                    throw new ControllerException("Credentials are not correct.");
                }

                return Ok(_tokenService.GenerateJWTToken(login.Email));
            }
            catch(ControllerException exception)
            {
                return Conflict(
                    new ErrorApiResponse<List<string> >( new List<string>(1) { exception.Message } )
                );
            }
        }

        [HttpPost("register")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Register([FromBody] LoginDto loginDto)
        {
            string hashedPassword = _passwordService.Hash(loginDto.Password);
            string token = _tokenService.GenerateJWTToken();

            UserEntity userEntity = new UserEntity(loginDto.Email, hashedPassword);
            userEntity.TokenEmailConfirmation = _tokenService.GenerateJWTToken();

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
            UserEntity userEntity = await _userService.GetUserByEmail(EmailOfCurrentUser);
            await _userService.DeleteUser(EmailOfCurrentUser);
            _storeService.DeleteDirectory(userEntity.Id.ToString());
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