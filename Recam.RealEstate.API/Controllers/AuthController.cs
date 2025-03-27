using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recam.RealEstate.API.DTOs;
using Recam.RealEstate.API.Models;
using Recam.RealEstate.API.Utils;
using System.Security.Claims;

namespace Recam.RealEstate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public AuthController(UserManager<User> userManager,
                              IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var user = new User
            {
                UserName = registerRequestDto.Username, // usernsme for login
                Name = registerRequestDto.Username, // user's real name that stored in database
                UserRole = registerRequestDto.UserRole
            };
            var result = await _userManager.CreateAsync(user, registerRequestDto.Password);
            if(result == null)
            {
                return BadRequest("Register user failed");
            }
            else if(result.Succeeded){
                return Ok();
            }
            else
            {
                return BadRequest("Register user failed");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByNameAsync(loginRequestDto.Username);
            if(user == null)
            {
                return Unauthorized();
            }
            var isAuthorized = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (isAuthorized)
            {
                var key = _configuration.GetSection("Jwt:SigningKey").Get<string>();
                var audience = _configuration.GetSection("Jwt:Audience").Get<string>();
                var issuer = _configuration.GetSection("Jwt:Issuer").Get<string>();
                if(key == null || audience == null || issuer == null)
                {
                    throw new Exception("Config can not be found");
                }
                var token = JwtUtils.GenerateToken(user, key, issuer, audience);
                return Ok(new
                {
                    token
                }); 
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpGet("currentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var user = await _userManager.FindByNameAsync(userName);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(new
            {
                user.UserName,
                user .UserRole,
                user.CompanyName,
                user.ProfileImageUrl
            });

        }
    }
}
