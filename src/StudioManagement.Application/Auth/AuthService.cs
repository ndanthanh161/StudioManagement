using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using StudioManagement.Application.Abstraction.Identity;
using StudioManagement.Application.Abstraction.Persistence;
using StudioManagement.Contract.DTO.Request;
using StudioManagement.Contract.DTO.Response;

namespace StudioManagement.Application.Auth
{
    public class AuthService(IUserRepository users, ITokenService token, ILogger<AuthService> logger) : IAuthService
    {
        private readonly PasswordHasher<string> _hasher = new();

        public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct = default)
        {
            var user = await users.FindByUserNameAsync(request.UserName, ct);

            if (user is null)
            {
                logger.LogWarning("Login failed: unknown user '{UserName}'", request.UserName);

                return null;
            }
            var pass = _hasher.VerifyHashedPassword(user.UserName, user.PasswordHash, request.Password);
            if (pass == PasswordVerificationResult.Failed)
            {
                logger.LogWarning("Login failed: bad password for '{UserName}'", request.UserName);

                return null;
            }
            var role = user.Role?.UserRole ?? "User";
            var (jwt, exp) = token.CreateToken(user.UserName, user.Email, user.FullName, role);
            return new LoginResponse
            {
                Email = user.Email,
                FullName = user.FullName,
                Token = jwt,
                UserName = user.UserName,
                Role = role,
                ExpiredAtUtc = exp
            };
        }
    }
}
