using Microsoft.AspNetCore.Mvc;
using StudioManagement.Application.Services.Rooms;
using StudioManagement.Contract.DTO.Request;

namespace StudioManagement.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController(RoomService room) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateRoomAsync([FromBody] RoomRequest request, CancellationToken ct = default)
        {
            try
            {
                var result = await room.AddAsync(request, ct);
                if (result is null)
                {
                    return NotFound(new { message = "Room not found" });
                }
                return Ok(new { message = "Room created successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }

        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllRoomsAsync(CancellationToken ct = default)
        {
            var rooms = await room.GetAllAsync(ct);
            return Ok(rooms);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRoomByIdAsync([FromRoute] int id, CancellationToken ct = default)
        {
            try
            {
                var roomDetails = await room.GetByIdAsync(id, ct);
                if (roomDetails is null)               // <-- sửa điều kiện null
                    return NotFound(new { message = "Room not found" });
                return Ok(roomDetails);
            }
            catch (InvalidOperationException ex)
            {

                return NotFound(new { messsage = ex.Message });
            }
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateRoomAsync([FromRoute] int id, [FromBody] RoomRequest request, CancellationToken ct = default)
        {
            try
            {
                var updatedRoom = await room.UpdateAsync(id, request, ct);
                if (updatedRoom is null)
                    return NoContent();
                return Ok(updatedRoom);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRoomAsync([FromRoute] int id, CancellationToken ct = default)
        {
            try
            {
                var isDeleted = await room.DeleteById(id, ct);
                if (!isDeleted)
                {
                    return NotFound(new { message = "Room not found" });
                }
                return Ok(new { message = "Room deleted successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }
    }
}
