using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Restaurant.IRepository;
using Restaurant.Repository.Repositories;

namespace Restaurant.Repository
{
    public static class RestaurantRepositoriesExtensions
    {
            public static IServiceCollection AddRestaurantRepositories(this IServiceCollection services)
            {
                services.TryAddScoped<ICityRepository, CityRepository>();

                return services;
            }
    }
}
