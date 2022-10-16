using System.Reflection;
using System.Text;

namespace Restaurant.LinqHelpers.Helpers
{
    public static class PagingExtension
    {
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> entities, int pageIndex = 0, int pageSize = 0)
        {
            if (!entities.Any())
            {
                return entities;
            }

            if(pageSize == 0)
            {
                return entities;
            }

            return entities
                .Skip(pageIndex * pageSize)
                .Take(pageSize);
        }
    }
}
