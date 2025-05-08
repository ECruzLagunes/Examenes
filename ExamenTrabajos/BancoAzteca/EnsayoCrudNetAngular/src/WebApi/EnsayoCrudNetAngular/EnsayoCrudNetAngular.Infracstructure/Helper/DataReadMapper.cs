// Proyecto: Infrastructure
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace EnsayoCrudNetAngular.Infrastructure.Helpers;

public static class DataReaderMapper
{
    public static List<T> MapToList<T>(this SqlDataReader reader) where T : new()
    {
        var results = new List<T>();
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var columnNames = Enumerable.Range(0, reader.FieldCount)
                                    .Select(reader.GetName)
                                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

        while (reader.Read())
        {
            var item = new T();
            foreach (var prop in properties)
            {
                if (!columnNames.Contains(prop.Name) || !prop.CanWrite)
                    continue;

                var value = reader[prop.Name];
                if (value == DBNull.Value)
                    continue;

                try
                {
                    prop.SetValue(item, Convert.ChangeType(value, prop.PropertyType));
                }
                catch
                {
                    continue; // O registrar el error
                }
            }

            results.Add(item);
        }

        return results;
    }

    public static T? MapToSingle<T>(this SqlDataReader reader) where T : new()
    {
        var list = reader.MapToList<T>();
        return list.FirstOrDefault();
    }

    public static T MapToEntity<T>(this IDataRecord record) where T : new()
    {
        var entity = new T();
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in properties)
        {
            if (!record.HasColumn(prop.Name) || record[prop.Name] is DBNull)
                continue;

            prop.SetValue(entity, Convert.ChangeType(record[prop.Name], prop.PropertyType));
        }

        return entity;
    }

    public static bool HasColumn(this IDataRecord record, string columnName)
    {
        for (int i = 0; i < record.FieldCount; i++)
        {
            if (record.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }
}
