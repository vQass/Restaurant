using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.APIComponents.Validators.CityValidators;
using Restaurant.APIComponents.Validators.IngredientValidators;
using Restaurant.APIComponents.Validators.MealCategoryValidators;
using Restaurant.APIComponents.Validators.MealValidators;
using Restaurant.APIComponents.Validators.OrderValidators.OrderCreate;
using Restaurant.APIComponents.Validators.OrderValidators.OrderUpdate;
using Restaurant.APIComponents.Validators.PromotionValidators;
using Restaurant.APIComponents.Validators.UserValidators;
using Restaurant.Data.Models.CityModels;
using Restaurant.Data.Models.IngredientModels;
using Restaurant.Data.Models.MealCategoryModels;
using Restaurant.Data.Models.MealModels;
using Restaurant.Data.Models.OrderModels;
using Restaurant.Data.Models.PromotionModels;
using Restaurant.Data.Models.UserModels;

namespace Restaurant.APIComponents
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

            //Promotions
            services.AddScoped<IValidator<PromotionCreateRequest>, PromotionCreateRequestValidator>();
            services.AddScoped<IValidator<PromotionUpdateRequest>, PromotionUpdateRequestValidator>();

            // Orders
            services.AddScoped<IValidator<OrderUpdateRequest>, OrderUpdateRequestValidator>();
            services.AddScoped<IValidator<OrderCreateRequest>, OrderCreateRequestValidator>();

            services.AddScoped<IValidator<OrderElementCreateRequest>, OrderElementCreateRequestValidator>();
            services.AddScoped<IValidator<OrderElementUpdateRequest>, OrderElementUpdateRequestValidator>();

            return services;
        }
    }
}
