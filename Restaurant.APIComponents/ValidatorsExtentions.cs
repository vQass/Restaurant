using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.APIComponents.Validators.MealValidators;
using Restaurant.APIComponents.Validators.UserValidators;
using Restaurant.Data.Models.MealModels;
using Restaurant.Data.Models.UserModels;

namespace Restaurant.APIComponents
{
    public static class ValidatorsExtentions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            //User
            services.AddScoped<IValidator<UserCreateRequest>, UserCreateRequestValidator>();
            services.AddScoped<IValidator<UserUpdateRequest>, UserUpdateRequestValidator>();
            
            //Meal
            services.AddScoped<IValidator<MealCreateRequest>, MealCreateRequestValidator>();
            services.AddScoped<IValidator<MealUpdateRequest>, MealUpdateRequestValidator>();



            return services;
        }
    }
}
