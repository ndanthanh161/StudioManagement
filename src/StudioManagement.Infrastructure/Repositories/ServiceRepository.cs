using Microsoft.EntityFrameworkCore;
using StudioManagement.Application.Abstraction.Persistence;
using StudioManagement.Domain.Entities;

namespace StudioManagement.Infrastructure.Repositories
{
    public class ServiceRepository(ApplicationDbContext db) : IServiceRepository
    {
        public async Task<IReadOnlyList<Service>> GetAllAsync(CancellationToken ct = default)
        {
            return await db.Services.ToListAsync(ct);
        }

        public async Task<Service> GetByIdAsync(int serviceId, CancellationToken ct = default)
        {
            var retsult = await db.Services.FirstOrDefaultAsync(s => s.ServiceId == serviceId, ct);
            if (retsult is null)
            {
                return null;
            }
            return retsult;
        }

        public async Task<Service> AddAsync(Service service, CancellationToken ct = default)
        {
            await db.Services.AddAsync(service, ct);
            await db.SaveChangesAsync(ct);
            return service;
        }

        public async Task<Service> UpdateAsync(Service update, CancellationToken ct = default)
        {
            var current = await db.Services.FirstOrDefaultAsync(s => s.ServiceId == update.ServiceId, ct);
            if (current is null)
            {
                return null;
            }
            current.ServiceName = update.ServiceName;
            current.ServicePrice = update.ServicePrice;
            current.Description = update.Description;
            await db.SaveChangesAsync(ct);
            return current;
        }

        public async Task<bool> DeleteAsync(int serviceId, CancellationToken ct = default)
        {
            var current = await db.Services.FirstOrDefaultAsync(s => s.ServiceId == serviceId, ct);
            if (current is null)
            {
                return false;
            }
            db.Services.Remove(current);
            await db.SaveChangesAsync(ct);
            return true;
        }
    }
}
