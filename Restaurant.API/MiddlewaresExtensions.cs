using Restaurant.API.Middlewares;

namespace Restaurant.API
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
