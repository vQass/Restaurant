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
                services.TryAddScoped<IIngredientRepository, IngredientRepository>();
                services.TryAddScoped<IMealCategoryRepository, MealCategoryRepository>();
                services.TryAddScoped<IMealRepository, MealRepository>();
                services.TryAddScoped<IRecipeRepository, RecipeRepository>();
                services.TryAddScoped<IUserRepository, UserRepository>();
                services.TryAddScoped<IOrderRepository, OrderRepository>();

                return services;
            }
    }
}
