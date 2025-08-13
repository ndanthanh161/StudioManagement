using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudioManagement.Application.Auth;
using StudioManagement.Contract.DTO.Request;

namespace StudioManagement.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController(IAuthService auth) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request, CancellationToken ct = default)
        {
            var result = await auth.RegisterAsync(request, ct);
            if (result is null)
            {
                return BadRequest("Registration failed. Please try again.");
            }
            return Ok(new { message = "Registration successful" });
        }
    }
}
