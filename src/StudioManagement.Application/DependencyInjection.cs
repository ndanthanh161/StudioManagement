using Microsoft.Extensions.DependencyInjection;
using StudioManagement.Application.Auth;

namespace StudioManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            service.AddScoped<IAuthService, AuthService>();
            return service;
        }
    }
}
