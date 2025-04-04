﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Recam.RealEstate.API.DTOs;
using Recam.RealEstate.API.Models;
using Recam.RealEstate.API.Utils;
using System.Security.Claims;

namespace Recam.RealEstate.API.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MongoDbContext _mongoDbContext;
        public AuthService(UserManager<User> userManager, 
                           IConfiguration configuration,
                           IMapper mapper,
                           IHttpContextAccessor httpContextAccessor,
                           MongoDbContext mongoDbContext)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _mongoDbContext = mongoDbContext;
        }

        public async Task<IdentityResult> Register(RegisterRequestDto registerRequestDto)
        {
            var user = new User
            {
                UserName = registerRequestDto.Username, // usernsme for login
                Name = registerRequestDto.Username, // user's real name 
                UserRole = registerRequestDto.UserRole
            };
            var result = await _userManager.CreateAsync(user, registerRequestDto.Password);

            await _mongoDbContext.UserActivityLogs.InsertOneAsync(new UserActivityLog
            {
                UserId = user.Id,
                Action = "Register",
                Resource = "User",
                Description = $"User {user.UserName} registered successfully!",
                Timestamp = DateTime.Now
            });

            return result;
        }

        public async Task<string?> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByNameAsync(loginRequestDto.Username);
            if (user == null)
            {
                return null;
            }
            var isAuthorized = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (!isAuthorized)
            {
                return null;
            }
            var key = _configuration.GetSection("Jwt:SigningKey").Get<string>();
            var audience = _configuration.GetSection("Jwt:Audience").Get<string>();
            var issuer = _configuration.GetSection("Jwt:Issuer").Get<string>();
            if (key == null || audience == null || issuer == null)
            {
                throw new Exception("Config can not be found");
            }
            var token = JwtUtils.GenerateToken(user, key, issuer, audience);

            await _mongoDbContext.UserActivityLogs.InsertOneAsync(new UserActivityLog
            {
                UserId = user.Id,
                Action = "Login",
                Resource = "User",
                Description = $"{user.UserName} successfully logged in",
                Timestamp = DateTime.Now
            });

            return token;  
        }

        public async Task<CurrentUserDto> GetCurrentUser()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
            if(userName == null)
            {
                return null;
            }
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return null; ;
            }
            return _mapper.Map<CurrentUserDto>(user);
        }


        public async Task<IdentityResult> SwitchUserRole(SwitchRoleDto switchRoleDto)
        {
            var user = await _userManager.FindByNameAsync(switchRoleDto.UserName);
            if(user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $"User {switchRoleDto.UserName} not found"
                });
            }
            user.UserRole = switchRoleDto.NewRole;

            await _mongoDbContext.UserActivityLogs.InsertOneAsync(new UserActivityLog
            {
                UserId = user.Id,
                Action = "Role Changed",
                Resource = "User",
                Description = $"User {user.UserName}'s role changed to {user.UserRole}",
                Timestamp = DateTime.Now
            });


            return await _userManager.UpdateAsync(user);
 
        }
    }
}
