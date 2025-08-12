using StudioManagement.Domain.Entities;

namespace StudioManagement.Application.Abstraction.Persistence
{
    public interface IUserRepository
    {
        Task<User?> FindByUserNameAsync(string userName, CancellationToken ct = default);
    }
}
