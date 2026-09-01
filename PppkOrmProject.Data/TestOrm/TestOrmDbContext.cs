using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Npgsql;
using PppkOrmProject.Data.TestOrm.Attributes;

namespace PppkOrmProject.Data.TestOrm;
using System.Reflection;

public class TestOrmDbContext<T> where T : class, new()
{
    private readonly string _connectionString;
    private readonly string _tableName;
    private readonly List<PropertyMapping> _propertyMappings;

    public TestOrmDbContext(string connectionString)
    {
        _connectionString = connectionString;
        
        var type = typeof(T);
        var tableAttribute = type.GetCustomAttribute<TableAttribute>() ??
                             throw new InvalidOperationException($"Class {type.Name} must have [Table] attribute.");
        _tableName = tableAttribute.Name;
        
        _propertyMappings = type.GetProperties()
            .Select(prop => new
            {
                Property = prop,
                Column = prop.GetCustomAttribute<ColumnAttribute>()
            })
            .Where(x => x.Column is not null)
            .Select(x => new PropertyMapping
            {
                Property = x.Property,
                ColumnName = x.Column!.Name,
                IsPrimaryKey = x.Column.IsPrimaryKey
            })
            .ToList();
        
        if (_propertyMappings.Count == 0)
        {
            throw new InvalidOperationException($"Class {type.Name} has no [Column] attributes.");
        }
    }
    
    public List<T> GetAll()
    {
        var columns = string.Join(", ", _propertyMappings.Select(m => $"\"{m.ColumnName}\""));
        var sql = $"SELECT {columns} FROM \"{_tableName}\"";

        System.Console.WriteLine($"TestOrm executing SQL: {sql}");

        var results = new List<T>();

        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var command = new NpgsqlCommand(sql, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var entity = new T();

            foreach (var mapping in _propertyMappings)
            {
                var value = reader[mapping.ColumnName];

                if (value == DBNull.Value)
                {
                    mapping.Property.SetValue(entity, null);
                }
                else
                {
                    mapping.Property.SetValue(entity, value);
                }
            }

            results.Add(entity);
        }

        return results;
    }

    private class PropertyMapping
    {
        public required PropertyInfo Property { get; init; }
        public required string ColumnName { get; init; }
        public bool IsPrimaryKey { get; init; }
    }
}

