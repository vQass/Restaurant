using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Restaurant.Services.Interfaces;
using Restaurant.Services.Services;

namespace Restaurant.Services
{
    public static class RestaurantServicesServiceCollectionExtensions
    {
        public static IServiceCollection AddRestaurantServices(this IServiceCollection services)
        {
            services.TryAddScoped<IUserService, UserService>();

            return services;
        }
    }
}
