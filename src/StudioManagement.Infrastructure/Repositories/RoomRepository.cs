using Microsoft.EntityFrameworkCore;
using StudioManagement.Application.Abstraction.Persistence;
using StudioManagement.Domain.Entities;

namespace StudioManagement.Infrastructure.Repositories
{
    public class RoomRepository(ApplicationDbContext db) : IRoomRepository
    {
        public async Task<Room> AddAsync(Room room, CancellationToken ct = default)
        {
            await db.Rooms.AddAsync(room, ct);
            await db.SaveChangesAsync(ct);
            return room;
        }

        public async Task<IReadOnlyList<Room>> GetAllRoomAsync(CancellationToken ct = default)
        => await db.Rooms.ToListAsync(ct);

        public async Task<Room?> GetByIdAsync(int roomId, CancellationToken ct = default)
        {
            var entity = await db.Rooms.FirstOrDefaultAsync(r => r.RoomId == roomId, ct);
            if (entity is null)
            {
                return null;
            }
            return entity;
        }

        public async Task<bool> DeleteByIdAsync(int roomId, CancellationToken ct = default)
        {
            var entity = await db.Rooms.FirstOrDefaultAsync(r => r.RoomId == roomId, ct);
            if (entity is null)
            {
                return false;
            }
            db.Rooms.Remove(entity);
            await db.SaveChangesAsync(ct);
            return true;
        }

        public async Task<Room> UpdateAsync(Room update, CancellationToken ct = default)
        {
            var old = await db.Rooms.FirstOrDefaultAsync(r => r.RoomId == update.RoomId, ct);
            if (old is null)
            {
                return null;
            }
            old.RoomName = update.RoomName;
            old.Quantity = update.Quantity;
            old.RoomPrice = update.RoomPrice;
            old.RoomStatus = update.RoomStatus;

            await db.SaveChangesAsync(ct);
            return old;
        }
    }
}
