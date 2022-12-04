using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Data.Models.CityModels;
using Restaurant.Data.Models.IngredientModels;
using Restaurant.Data.Models.MealCategoryModels;
using Restaurant.Data.Models.MealModels;
using Restaurant.Data.Models.OrderModels;
using Restaurant.Data.Models.UserModels.Requests;
using Restaurant.Validators.FluentValidators.CityValidators;
using Restaurant.Validators.FluentValidators.IngredientValidators;
using Restaurant.Validators.FluentValidators.MealCategoryValidators;
using Restaurant.Validators.FluentValidators.MealValidators;
using Restaurant.Validators.FluentValidators.OrderValidators.OrderCreate;
using Restaurant.Validators.FluentValidators.OrderValidators.OrderUpdate;
using Restaurant.Validators.FluentValidators.UserValidators;

namespace Restaurant.Validators
{
    public static class ValidatorsExtentions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            //City
            services.AddScoped<IValidator<CityCreateRequest>, CityCreateRequestValidator>();
            services.AddScoped<IValidator<CityUpdateRequest>, CityUpdateRequestValidator>();

            //Ingredient
            services.AddScoped<IValidator<IngredientCreateRequest>, IngredientCreateRequestValidator>();
            services.AddScoped<IValidator<IngredientUpdateRequest>, IngredientUpdateRequestValidator>();

            //MealCategory
            services.AddScoped<IValidator<MealCategoryCreateRequest>, MealCategoryCreateRequestValidator>();
            services.AddScoped<IValidator<MealCategoryUpdateRequest>, MealCategoryUpdateRequestValidator>();

            //User
            services.AddScoped<IValidator<UserCreateRequest>, UserCreateRequestValidator>();
            services.AddScoped<IValidator<UserUpdateRequest>, UserUpdateRequestValidator>();

            //Meal
            services.AddScoped<IValidator<MealCreateRequest>, MealCreateRequestValidator>();
            services.AddScoped<IValidator<MealUpdateRequest>, MealUpdateRequestValidator>();

            // Orders
            services.AddScoped<IValidator<OrderUpdateRequest>, OrderUpdateRequestValidator>();
            services.AddScoped<IValidator<OrderCreateRequest>, OrderCreateRequestValidator>();

            services.AddScoped<IValidator<OrderElementCreateRequest>, OrderElementCreateRequestValidator>();
            services.AddScoped<IValidator<OrderElementUpdateRequest>, OrderElementUpdateRequestValidator>();

            return services;
        }
    }
}
