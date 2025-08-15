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

        public Task<bool> ExistByUserNameAsync(string userName, CancellationToken ct = default)
        {
            return db.Users.AnyAsync(u => u.UserName == userName, ct);
        }
        public Task<bool> ExistByEmailAsync(string email, CancellationToken ct = default)
        {
            return db.Users.AnyAsync(u => u.Email == email, ct);
        }

        public Task<int?> GetUserRole(string role, CancellationToken ct = default)
        {
            var rn = role.Trim().ToUpper();
            return db.Roles.Where(r => r.UserRole.ToUpper() == rn)
                           .Select(r => (int?)r.RoleId)
                           .FirstOrDefaultAsync(ct);
        }
        public Task AddAsync(User user, CancellationToken ct = default)
        {
            db.Users.Add(user);
            return db.SaveChangesAsync(ct);
        }
    }
}
