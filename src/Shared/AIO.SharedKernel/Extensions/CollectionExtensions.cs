using Microsoft.EntityFrameworkCore;

namespace AIO.SharedKernel.Extensions;

public static class CollectionExtensions
{
    public static IQueryable<T> QueryBuilder<T>(this IQueryable<T> query, int pageSize, int pageNo, bool all,
        Dictionary<string, List<(string, string)>> filters)
    {
        var propertyInfo = typeof(T).GetProperties()
            .ToDictionary(p => p.Name.ToLowerInvariant(), p => p.Name);

        foreach (var (propertyName, filterConditions) in filters)
        {
            if (!propertyInfo.TryGetValue(propertyName.ToLowerInvariant(), out var realPropertyName)) continue;
            foreach (var (operatorType, value) in filterConditions)
            {
                query = operatorType switch
                {
                    "like" => query.Where(u => u != null && EF.Property<string>(u, realPropertyName).Contains(value)),
                    "==" => query.Where(u => u != null && EF.Property<string>(u, realPropertyName) == value),
                    "!=" => query.Where(u => u != null && EF.Property<string>(u, realPropertyName) != value),
                    ">" => query.Where(u => u != null && EF.Property<int>(u, realPropertyName) > int.Parse(value)),
                    "<" => query.Where(u => u != null && EF.Property<int>(u, realPropertyName) < int.Parse(value)),
                    ">=" => query.Where(u => u != null && EF.Property<int>(u, realPropertyName) >= int.Parse(value)),
                    "<=" => query.Where(u => u != null && EF.Property<int>(u, realPropertyName) < int.Parse(value)),
                    _ => query
                };
            }
        }

        if (all)
            return query;

        var skipCount = pageSize * (pageNo - 1);
        query = query.Skip(skipCount).Take(pageSize);
        return query;
    }
}