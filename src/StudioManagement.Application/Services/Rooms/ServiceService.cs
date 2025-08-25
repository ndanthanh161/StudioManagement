using Microsoft.Extensions.Logging;
using StudioManagement.Application.Abstraction.Persistence;
using StudioManagement.Contract.DTO.Request;
using StudioManagement.Domain.Entities;

namespace StudioManagement.Application.Services.Rooms
{
    public class ServiceService(IServiceRepository service, ILogger<ServiceService> logger)
    {
        public Task<IReadOnlyList<Service>> GetAllAsync(CancellationToken ct = default)
        {
            return service.GetAllAsync(ct);
        }

        public async Task<Service?> GetByIdAsync(int serviceId, CancellationToken ct = default)
        {
            var item = await service.GetByIdAsync(serviceId, ct);
            if (item is null)
            {
                logger.LogWarning("Service with ID {ServiceId} not found.", serviceId);
                return null;
            }
            return item;
        }

        public async Task<string> AddAsync(ServiceRequest request, CancellationToken ct = default)
        {
            var created = new Service
            {

                ServiceName = request.ServiceName,

                ServicePrice = request.ServicePrice,

                Description = request.Description
            };
            await service.AddAsync(created, ct);
            logger.LogInformation("Service created successfully: {ServiceName}", created.ServiceName);
            return created.ServiceId.ToString();
        }
        public async Task<Service> UpdateAsync(int serviceId, ServiceRequest update, CancellationToken ct = default)
        {
            var exist = await service.GetByIdAsync(serviceId, ct);
            if (exist is null)
            {
                logger.LogWarning("Service with ID {ServiceId} not found for update.", serviceId);
                return null;
            }
            bool changed = false;
            if (!string.Equals(exist.ServiceName, update.ServiceName, StringComparison.Ordinal))
            {
                exist.ServiceName = update.ServiceName;
                changed = true;
            }
            if (exist.ServicePrice != update.ServicePrice)
            {
                exist.ServicePrice = update.ServicePrice;
                changed = true;
            }
            if (!string.Equals(exist.Description, update.Description, StringComparison.Ordinal))
            {
                exist.Description = update.Description;
                changed = true;
            }
            if (!changed)
            {
                return exist;
            }

            var updated = await service.UpdateAsync(exist, ct);
            return updated;
        }
        public async Task<bool> DeleteAsync(int serviceId, CancellationToken ct = default)
        {
            var exist = await service.DeleteAsync(serviceId, ct);
            if (!exist)
            {
                logger.LogInformation("Service not found!");
                return false;
            }
            logger.LogInformation("Service deleted successfully!");
            return true;
        }
    }
}
