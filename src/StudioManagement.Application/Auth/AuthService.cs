using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using StudioManagement.Application.Abstraction.Identity;
using StudioManagement.Application.Abstraction.Persistence;
using StudioManagement.Contract.DTO.Request;
using StudioManagement.Contract.DTO.Response;
using StudioManagement.Domain.Entities;

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
        public async Task <string>RegisterAsync(RegisterRequest request, CancellationToken ct = default)
        {
            if(await users.ExistByUserNameAsync(request.UserName, ct))
            {
                logger.LogWarning("Registration failed: user '{UserName}' already exists", request.UserName);
                throw new InvalidOperationException($"User '{request.UserName}' already exists.");
            }
            if(await users.ExistByEmailAsync(request.Email, ct))
            {
                logger.LogWarning("Registration failed: email '{Email}' already exists", request.Email);
                throw new InvalidOperationException($"Email '{request.Email}' already exists.");
            }
            var pass = _hasher.HashPassword(request.UserName, request.Password);
            var user = new User
            {
                UserName = request.UserName.Trim(),
                Email = request.Email.Trim(),
                FullName = request.FullName.Trim(),
                Phone = request.Phone.Trim(),
                PasswordHash = pass
            };

            await users.AddAsync(user, ct);
            logger.LogInformation("User '{UserName}' registered successfully", request.UserName);
            return user.UserId.ToString();
        }
    }
}
