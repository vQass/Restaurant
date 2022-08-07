﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Restaurant.IServices;
using Restaurant.Services.Services;

namespace Restaurant.Services
{
    public static class RestaurantServicesExtentions
    {
        public static IServiceCollection AddRestaurantServices(this IServiceCollection services)
        {
            services.TryAddScoped<IUserService, UserService>();
            services.TryAddScoped<IMealService, MealService>();
            services.TryAddScoped<IMealCategoryService, MealCategoryService>();
            services.TryAddScoped<IIngredientService, IngredientService>();
            services.TryAddScoped<ISeederService, SeederService>();

            return services;
        }
    }
}
