using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudioManagement.Application.Abstraction.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudioManagement.Infrastructure.Identity
{
    public class TokenService(IOptions<JwtOptions> otp) : ITokenService
    {
        private readonly JwtOptions _otp = otp.Value;

        public (string Token, DateTime ExpiredAtUtc) CreateToken(string UserName, string Email, string FullName, string Role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_otp.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("username", UserName),
                new Claim("email", Email),
                new Claim("fullname", FullName),
                new Claim("role", Role),
            };

            var exp = DateTime.UtcNow.AddMinutes(_otp.ExpiryMinutes);
            var token = new JwtSecurityToken(
                issuer: _otp.Issuer,
                audience: _otp.Audience,
                claims: claims,
                expires: exp,
                signingCredentials: creds
            );

            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            return (tokenStr, exp);
        }
    }
}
