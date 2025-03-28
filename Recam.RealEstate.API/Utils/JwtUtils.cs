using Microsoft.IdentityModel.Tokens;
using Recam.RealEstate.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Recam.RealEstate.API.Utils
{
    public class JwtUtils
    {
        public static string GenerateToken(User user , string key, string issuer, string audience)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.UserRole)
            };
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
            (
                issuer: issuer,
                audience: audience,
                claims: claims,
                signingCredentials:creds,
                expires:DateTime.Now.AddMinutes(15)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
