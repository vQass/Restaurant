using Microsoft.Extensions.DependencyInjection;
using Restaurant.APIComponents.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
