using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordiaLib.DataBase;

public class SqlReader
{
    private readonly string _connectionString;

    public SqlReader()//IOptions<ConnectionStringOptions> options
    {
        //_connectionString = options.Value.DefaultDatabase;
        _connectionString = $"Data Source = DESKTOP - 617MC8M\\SQLEXPRESS; Initial Catalog = ConcordiaDB; Integrated Security = True";
    }

    public async IEnumerable<T> QueryAsync<T>(string query)
    {
        var entities = new HashSet<T>();

        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(query, connection);

        await connection.OpenAsync();
        await using var dr = await command.ExecuteReaderAsync();
        while (await dr.ReadAsync())
        {
            var entity = new <T>(dr);
            entities.Add(entity);
        }

        await connection.CloseAsync();
        await connection.DisposeAsync();

        return entities;
    }

    public async Task<T?> QueryOnceAsync<T>(string query)
    {
        T? result = default(T);

        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(query, connection);

        await connection.OpenAsync();
        await using var dr = await command.ExecuteReaderAsync();
        if (await dr.ReadAsync()) result = new T(dr);

        await connection.CloseAsync();
        await connection.DisposeAsync();

        return result;
    }
}

