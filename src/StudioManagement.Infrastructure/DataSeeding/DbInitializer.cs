using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudioManagement.Domain.Entities;   // ApplicationDbContext

namespace StudioManagement.Infrastructure.DataSeeding
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            // Seed Role ADMIN
            var roles = new[] { "ADMIN", "CUSTOMER", "STAFF" };

            foreach (var roleName in roles)
            {
                if (!await db.Roles.AnyAsync(r => r.UserRole == roleName))
                {
                    db.Roles.Add(new Role { UserRole = roleName });
                }
            }
            await db.SaveChangesAsync();

            var adminRole = await db.Roles.FirstOrDefaultAsync(r => r.UserRole == "ADMIN");
            if (adminRole == null)
            {
                adminRole = new Role { UserRole = "ADMIN" };
                db.Roles.Add(adminRole);
                await db.SaveChangesAsync();
            }

            // Seed Admin user
            var adminEmail = "admin@bookingstudio.local"; // nên đọc từ IConfiguration
            var adminUser = await db.Users.FirstOrDefaultAsync(u => u.Email == adminEmail);
            if (adminUser == null)
            {
                var hasher = new PasswordHasher<User>();
                var user = new User
                {
                    UserName = "admin",
                    Email = adminEmail,
                    FullName = "System Administrator",
                    Phone = "0900000000",
                    RoleId = adminRole.RoleId
                };
                user.PasswordHash = hasher.HashPassword(user, "admin123"); // nên lấy từ secret/env

                db.Users.Add(user);
                await db.SaveChangesAsync();
            }
        }
    }
}
