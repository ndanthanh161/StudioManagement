using Microsoft.Extensions.Logging;
using StudioManagement.Application.Abstraction.Persistence;
using StudioManagement.Contract.DTO.Request;
using StudioManagement.Domain.Entities;

namespace StudioManagement.Application.Services.Rooms
{
    public class RoomService(IRoomRepository rooms, ILogger<RoomService> logger)
    {

        public Task<IReadOnlyList<Room>> GetAllAsync(CancellationToken ct = default)
        {
            return rooms.GetAllRoomAsync(ct);
        }

        public async Task<Room?> GetByIdAsync(int roomId, CancellationToken ct = default)
        {
            var room = await rooms.GetByIdAsync(roomId, ct);
            if (room is null)
            {
                logger.LogWarning("Room with ID {RoomId} not found.", roomId);
                return null;
            }
            return room;
        }
        public async Task<string> AddAsync(RoomRequest request, CancellationToken ct = default)
        {
            var room = new Room
            {
                RoomName = request.RoomName,
                Quantity = request.Quantity,
                RoomPrice = request.RoomPrice,
            };

            await rooms.AddAsync(room, ct);
            logger.LogInformation("Room created successfully: {RoomName}", room.RoomName);
            return room.RoomId.ToString();
        }

        public async Task<Room> UpdateAsync(int roomId, RoomRequest request, CancellationToken ct = default)
        {
            var existingRoom = await rooms.GetByIdAsync(roomId, ct);
            if (existingRoom is null)
            {
                logger.LogWarning("Room with ID {RoomId} not found for update.", roomId);
                return null;
            }

            // Nếu muốn "không đổi thì không làm gì":
            bool changed = false;
            if (!string.Equals(existingRoom.RoomName, request.RoomName, StringComparison.Ordinal))
            { existingRoom.RoomName = request.RoomName; changed = true; }

            if (existingRoom.Quantity != request.Quantity)
            { existingRoom.Quantity = request.Quantity; changed = true; }

            if (existingRoom.RoomPrice != request.RoomPrice)
            { existingRoom.RoomPrice = request.RoomPrice; changed = true; }

            if (!changed)
            {
                logger.LogInformation("No changes for room {RoomId}. Skipping update.", roomId);
                return null; // Controller sẽ trả 204 No Content
            }

            var updated = await rooms.UpdateAsync(existingRoom, ct);
            return updated;
        }

        public async Task<bool> DeleteById(int roomId, CancellationToken ct = default)
        {
            var isDeleted = await rooms.DeleteByIdAsync(roomId, ct);
            if (!isDeleted)
            {
                logger.LogWarning("Failed to delete room with ID {RoomId}. Room not found.", roomId);
                return false;
            }
            logger.LogInformation("Room with ID {RoomId} deleted successfully.", roomId);
            return true;
        }
    }
}
