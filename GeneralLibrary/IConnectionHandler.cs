using Npgsql;

namespace GeneralLibrary
{
    public interface IConnectionHandler
    {
        NpgsqlConnection Create(string connectionString = null);
    }
}
