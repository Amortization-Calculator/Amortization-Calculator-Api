using Amortization_Calculator_Api.Dtos;
using Amortization_Calculator_Api.Services.auth;
using Microsoft.AspNetCore.Mvc;

namespace Amortization_Calculator_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userResponse = await _authService.RegisterUserAsync(registerDto);

            if (userResponse == null)
            {
                return Conflict(new { message = "Email is already in use." });
            }

            return Ok(userResponse);
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userResponse = await _authService.LoginUserAsync(loginDto);

            if (userResponse == null)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            return Ok(userResponse);
        }



    }
}
