using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioManagement.Application.Services.Auth;
using StudioManagement.Contract.DTO.Request;

namespace StudioManagement.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService auth) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request, CancellationToken ct = default)
        {
            var result = await auth.LoginAsync(request, ct);
            if (result is null) return Unauthorized("Invalid username or password.");
            return Ok(new
            {
                message = "Đăng nhập thành công",
                data = new { accessToken = result.Token }
            });
        }
    }
}
