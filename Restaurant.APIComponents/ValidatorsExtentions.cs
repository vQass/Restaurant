using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.APIComponents.Validators;
using Restaurant.Data.Models.UserModels;

namespace Restaurant.APIComponents
{
    public static class ValidatorsExtentions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<UserCreateRequestDto>, UserCreateRequestDtoValidator>();

            return services;
        }
    }
}
