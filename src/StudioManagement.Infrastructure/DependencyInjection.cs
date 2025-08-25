using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudioManagement.Application.Abstraction.Identity;
using StudioManagement.Application.Abstraction.Persistence;
using StudioManagement.Infrastructure.Identity;
using StudioManagement.Infrastructure.Repositories;

namespace StudioManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration cfg)
        {
            services.Configure<JwtOptions>(cfg.GetSection("Jwt"));
            services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseSqlServer(cfg.GetConnectionString("Default")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();

            return services;
        }
    }
}
