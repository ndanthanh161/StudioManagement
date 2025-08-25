using StudioManagement.Domain.Entities;

namespace StudioManagement.Application.Abstraction.Persistence
{
    public interface IRoomRepository
    {
        Task<Room> AddAsync(Room room, CancellationToken ct = default);

        Task<IReadOnlyList<Room>> GetAllRoomAsync(CancellationToken ct = default);
        Task<Room?> GetByIdAsync(int roomId, CancellationToken ct = default);
        Task<bool> DeleteByIdAsync(int roomId, CancellationToken ct = default);

        Task<Room> UpdateAsync(Room room, CancellationToken ct = default);
    }
}
