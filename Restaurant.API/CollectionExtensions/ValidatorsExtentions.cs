using FluentValidation;
using Restaurant.Data.Models.UserModels;
using Restaurant.Data.Validators;

namespace Restaurant.API.CollectionExtensions
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
