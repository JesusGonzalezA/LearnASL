using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Core.Contracts.OutComing;
using Core.Entities;
using Core.Interfaces;
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
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("me")]
        [ProducesResponseType(typeof(UserDto),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Get()
        {
            UserEntity userEntity = await _userService.GetUserByEmail(EmailOfCurrentUser);
            
            UserDto userDto = _mapper.Map<UserDto>(userEntity);
            return Ok(userDto);
        }
    }
}
