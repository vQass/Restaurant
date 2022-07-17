using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Data.Models.UserModels;
using Restaurant.Data.Validators;

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
