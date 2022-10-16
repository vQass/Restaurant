using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Restaurant.LinqHelpers.Helpers;
using Restaurant.LinqHelpers.Interfaces;

namespace Restaurant.LinqHelpers
{
    public static class LinqHelpersExtentions
    {
        public static IServiceCollection AddLinqHelpers(this IServiceCollection services)
        {
            services.TryAddScoped<ISortingHelper, SortingHelper>();

            return services;
        }
    }
}
