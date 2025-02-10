using Authentication.Application.DTOs;
using Authentication.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Authentication.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private IAccountService accountService;
        public AuthController(IAccountService _accountService)
        {
            accountService = _accountService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<Response>> Register([FromBody] CreateUserDTO userDTO)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var response = await accountService.Register(userDTO);
            return response != null ? Ok(response): BadRequest(Request);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Response>> Login([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await accountService.Login(loginDto);
            return response != null ? Ok(response) : BadRequest(Request);
        }

        [HttpPost("update-profile")]
        [Authorize]
        public async Task<ActionResult<Response>> UpdateProfile([FromBody] CreateUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updateUserDto = new UpdateUserDTO(user.Password, user.Name, user.Phone);
            var response = await accountService.UpdateProfile(user.Email, updateUserDto);
            return response != null ? Ok(response) : BadRequest(Request);
        }

    }
}
