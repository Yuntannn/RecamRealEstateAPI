using Microsoft.AspNetCore.Identity;
using Recam.RealEstate.API.DTOs;
using Recam.RealEstate.API.Models;

namespace Recam.RealEstate.API.Services.AuthService
{
    public interface IAuthService
    {
        Task<IdentityResult> Register(RegisterRequestDto registerRequestDto);
        Task<string?> Login(LoginRequestDto loginRequestDto);
        Task<CurrentUserDto> GetCurrentUser();
        Task<IdentityResult> SwitchUserRole(SwitchRoleDto switchRoleDto);
    }
}
