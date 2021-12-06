using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using System.Threading.Tasks;
using Core.Contracts.Incoming;
using System.Net;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController
        (
            IUserService userService,
            ITokenService tokenService
        )
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Create([FromBody] LoginDto login)
        {
            bool match = await _userService.CheckCredentials(login.Email, login.Password);
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
    }
}