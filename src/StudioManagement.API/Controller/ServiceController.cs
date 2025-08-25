using Microsoft.AspNetCore.Mvc;
using StudioManagement.Application.Services.Rooms;
using StudioManagement.Contract.DTO.Request;

namespace StudioManagement.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController(ServiceService service) : ControllerBase
    {
        [HttpGet("All")]
        public async Task<IActionResult> GetAllAsync(CancellationToken ct = default)
        {
            var services = await service.GetAllAsync(ct);
            return Ok(services);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, CancellationToken ct = default)
        {
            try
            {
                var serviceId = await service.GetByIdAsync(id, ct);
                if (serviceId is null)
                    return NotFound(new { message = "Service not found" });
                return Ok(serviceId);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });

            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> AddAsync([FromBody] ServiceRequest request, CancellationToken ct = default)
        {
            try
            {
                var result = await service.AddAsync(request, ct);
                if (result is null)
                {
                    return NotFound(new { message = "Service invalid" });
                }

                return Ok(new { message = "Service created successfully!" });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ServiceRequest request, CancellationToken ct = default)
        {
            try
            {
                var updated = await service.UpdateAsync(id, request, ct);
                if (updated is null)
                    return NoContent();
                return Ok(updated);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken ct = default)
        {
            try
            {
                var deleted = await service.DeleteAsync(id, ct);
                if (!deleted)
                {
                    return NotFound(new { message = "Service not found" });
                }
                return Ok(new { message = "Service deleted successfully!" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
