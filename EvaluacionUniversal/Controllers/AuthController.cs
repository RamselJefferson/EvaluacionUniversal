using Core.Application.Interfaces.Services;
using Core.Application.Request;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionUniversal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService){_userService = userService;}


        [HttpPost("register")]
        public  async Task<IActionResult> Register(UserCreateRequest request)
        {
            var entity = await _userService.AddAsync(request);
            return Ok(entity);
        }


        [HttpPost("login")]
        public  async Task<IActionResult> Login(UserRequest request)
        {
            var user = await _userService.Login(request);
            return Ok(user);
        }


    }
}
