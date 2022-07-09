using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Restaurant.Services.Interfaces;
using Restaurant.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services
{
    public static class RestaurantServicesServiceCollectionExtensions
    {
        public static IServiceCollection AddRestaurantServices(this IServiceCollection services)
        {
            services.TryAddScoped<IUserService, UserService>();

            return services;
        }
    }
}
