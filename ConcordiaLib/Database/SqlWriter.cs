using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConcordiaLib.Abstract;
using System.Data;
using System.Data.SqlClient;

namespace ConcordiaLib.Database;
  
public class SqlWriter : IWriter
{
    private readonly string _connectionString;

    public SqlWriter()//IOptions<ConnectionStringOptions> options
    {
        //_connectionString = options.Value.DefaultDatabase;
        _connectionString = $"Data Source = DESKTOP - 617MC8M\\SQLEXPRESS; Initial Catalog = ConcordiaDB; Integrated Security = True";
    }

    public async Task<bool> WriteAsync(string query, IEnumerable<(string, object?)> parameters)
    {
        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(query, connection);

        foreach (var p in parameters) command.Parameters.Add(new SqlParameter(p.Item1, p.Item2));

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();

        await connection.CloseAsync();
        await connection.DisposeAsync();

        return true;
    }

    public Task<bool> WriteAsync(string query, List<(string, object?)> parameters)
    {
        throw new NotImplementedException();
    }
}