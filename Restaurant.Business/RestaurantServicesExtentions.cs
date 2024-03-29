﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Restaurant.Business.IServices;
using Restaurant.Business.Services;

namespace Restaurant.Business
{
    public static class RestaurantServicesExtentions
    {
        public static IServiceCollection AddRestaurantServices(this IServiceCollection services)
        {
            services.TryAddScoped<ICityService, CityService>();
            services.TryAddScoped<IIngredientService, IngredientService>();
            services.TryAddScoped<IMealCategoryService, MealCategoryService>();
            services.TryAddScoped<IMealService, MealService>();
            services.TryAddScoped<IOrderService, OrderService>();
            services.TryAddScoped<IRecipeService, RecipeService>();
            services.TryAddScoped<IUserService, UserService>();

            services.TryAddScoped<ISeederService, SeederService>();

            return services;
        }
    }
}
