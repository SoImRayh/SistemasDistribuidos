using Npgsql;
using System.Data;

namespace APICatalogo.Context;

public class ApiCatalogoDbSession : IDisposable
{
    public IDbConnection Connection { get; private set; }

    public ApiCatalogoDbSession(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConection");
        Connection = new NpgsqlConnection(connectionString);

        Connection.Open();
    }

    public void Dispose() => Connection?.Close();
}
