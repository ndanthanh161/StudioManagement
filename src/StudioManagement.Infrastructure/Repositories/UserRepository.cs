using Microsoft.EntityFrameworkCore;
using StudioManagement.Application.Abstraction.Persistence;
using StudioManagement.Domain.Entities;
namespace StudioManagement.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext db) : IUserRepository
    {
        public Task<User?> FindByUserNameAsync(string userName, CancellationToken ct = default)
        {
            return db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserName == userName, ct);
        }
    }
}
