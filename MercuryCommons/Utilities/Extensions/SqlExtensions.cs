using System.Threading.Tasks;
using MySqlConnector;

namespace MercuryCommons.Utilities.Extensions;

public static class SqlExtensions
{
    public static async Task<MySqlDataReader> ExecuteReaderAsync(this MySqlConnection connection, string command, MySqlParameter[] parameters = null)
    {
        var ex = new MySqlCommand(command, connection);
        if (parameters is { Length: > 0 }) ex.Parameters.AddRange(parameters);
        var reader = await ex.ExecuteReaderAsync();
        ex.Dispose();
        return reader;
    }

    public static void ExecuteNoQuery(this MySqlConnection connection, string command, MySqlParameter[] parameters = null)
    {
        var ex = new MySqlCommand(command, connection);
        if (parameters is { Length: > 0 }) ex.Parameters.AddRange(parameters);
        ex.ExecuteNonQuery();
        ex.Dispose();
    }
}