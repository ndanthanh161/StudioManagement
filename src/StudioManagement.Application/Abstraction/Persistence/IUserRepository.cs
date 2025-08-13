using StudioManagement.Domain.Entities;

namespace StudioManagement.Application.Abstraction.Persistence
{
    public interface IUserRepository
    {
        Task<User?> FindByUserNameAsync(string userName, CancellationToken ct = default);
        Task<bool> ExistByUserNameAsync(string userName, CancellationToken ct = default);
        Task<bool> ExistByEmailAsync(string email, CancellationToken ct = default);
        Task AddAsync(User user, CancellationToken ct = default);

    }
}
