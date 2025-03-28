using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recam.RealEstate.API.DTOs;
using Recam.RealEstate.API.Models;
using Recam.RealEstate.API.Services.AuthService;
using Recam.RealEstate.API.Utils;
using System.Security.Claims;

namespace Recam.RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var result = await _authService.Register(registerRequestDto);
            if (result.Succeeded)
            {
                return Ok("User registered successfully");
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginRequestDto loginRequestDto)
        {
            var token = await _authService.Login(loginRequestDto);
            if(token == null)
            {
                return Unauthorized("Invalid username or password");
            }
            return Ok(new
            {
                token
            });
        }

        [Authorize]
        [HttpGet("currentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var result = await _authService.GetCurrentUser();
            if(result == null)
            {
                return BadRequest("No user found");
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("switchrole")]
        public async Task<IActionResult> SwitchUserRole([FromBody] SwitchRoleDto switchRoleDto)
        {
            var result = await _authService.SwitchUserRole(switchRoleDto);
            if (!result.Succeeded)
            {
                return BadRequest("Update role failed");
            }
            return Ok($"Role updated to {switchRoleDto.NewRole} for {switchRoleDto.UserName}");
        }
    }
}
