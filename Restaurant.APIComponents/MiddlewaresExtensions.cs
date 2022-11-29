using Microsoft.Extensions.DependencyInjection;
using Restaurant.APIComponents.Middlewares;

namespace Restaurant.APIComponents
{
    public static class MiddlewaresExtensions
    {
        public static IServiceCollection AddMiddlewares(this IServiceCollection services)
        {
            services.AddScoped<ErrorHandlingMiddleware>();

            return services;
        }
    }
}
