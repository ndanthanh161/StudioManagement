using Microsoft.Extensions.DependencyInjection;
using StudioManagement.Application.Services.Auth;
using StudioManagement.Application.Services.Rooms;

namespace StudioManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<RoomService>();

            return service;
        }
    }
}
