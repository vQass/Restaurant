using Microsoft.Extensions.DependencyInjection.Extensions;
using Restaurant.IServices.Interfaces;
using Restaurant.Services.Services;

namespace Restaurant.API.CollectionExtensions
{
    public static class RestaurantServicesExtentions
    {
        public static IServiceCollection AddRestaurantServices(this IServiceCollection services)
        {
            services.TryAddScoped<IUserService, UserService>();
            services.TryAddScoped<ISeederService, SeederService>();

            return services;
        }
    }
}
