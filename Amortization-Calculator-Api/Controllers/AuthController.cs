﻿using Amortization_Calculator_Api.Dtos;
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

            if (!userResponse.isAuthSuccessful)
            {
                return Conflict(new { message = userResponse.Message });
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
                return Unauthorized(new { message = "Invalid username or password or user unactive." });
            }

            return Ok(new {acssesToken = userResponse.token , expierAt = userResponse.expireDate , userGender = userResponse.gender , name=userResponse.userName});
        }



    }
}
