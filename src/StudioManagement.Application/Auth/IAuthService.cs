using StudioManagement.Contract.DTO.Request;
using StudioManagement.Contract.DTO.Response;

namespace StudioManagement.Application.Auth
{
    public interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request, CancellationToken ct = default);
    }
}
