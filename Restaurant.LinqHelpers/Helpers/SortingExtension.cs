﻿using Restaurant.LinqHelpers.Models;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace Restaurant.LinqHelpers.Helpers
{
    public static class SortingExtension
    {
        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> entities, string orderByQueryString)
        {
            if (!entities.Any())
            {
                return entities;
            }

            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return entities;
            }

            var paramsWithSortDirection = GetSortParams(orderByQueryString);

            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in paramsWithSortDirection)
            {
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(param.PropertyName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                {
                    continue;
                }

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {param.SortDirection}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            return entities.OrderBy(orderQuery);
        }

        private static List<SortParam> GetSortParams(string orderParams)
        {
            var splittedParams = GetValidParams(orderParams);

            var paramsWithSortDir = new List<SortParam>();

            foreach (var param in splittedParams)
            {
                paramsWithSortDir.Add(new SortParam
                {
                    PropertyName = param.Split(" ")[0],
                    SortDirection = param.EndsWith(" desc") ? "descending" : "ascending"
                });
            }

            return paramsWithSortDir;
        }

        private static List<string> GetValidParams(string queryString)
        {
            var splittedQuery = queryString
                .Trim()
                .Split(",")
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();

            for (int i = 0; i < splittedQuery.Count; i++)
            {
                splittedQuery[i] = splittedQuery[i].Trim();
            }

            return splittedQuery;
        }
    }
}