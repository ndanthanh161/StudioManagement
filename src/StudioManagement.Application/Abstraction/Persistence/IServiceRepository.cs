using StudioManagement.Domain.Entities;

namespace StudioManagement.Application.Abstraction.Persistence
{
    public interface IServiceRepository
    {
        Task<IReadOnlyList<Service>> GetAllAsync(CancellationToken ct = default);
        Task<Service> GetByIdAsync(int serviceId, CancellationToken ct = default);
        Task<Service> AddAsync(Service service, CancellationToken ct = default);
        Task<Service> UpdateAsync(Service service, CancellationToken ct = default);
        Task<bool> DeleteAsync(int serviceId, CancellationToken ct = default);
    }
}
