using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myproject.DAOs;
using myproject.Model;
using myproject.Services.Interfaces;

namespace myproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/register")]
        public async Task<ActionResult> register([FromBody]RegisterUserDTO registerUserDAO)
        {
            return Ok(await _userService.register(registerUserDAO));
        }

        [HttpPost("/login")]
        public async Task<ActionResult<string>> login([FromBody] LoginUserDTO loginUserDAO)
        {
            return Ok(await _userService.login(loginUserDAO));
        }


    }
}
