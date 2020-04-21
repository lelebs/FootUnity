using Npgsql;
using System;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace GeneralLibrary
{
    public class ConnectionHandler : IDbConnection, IConnectionHandler
    {
        public string Padrao { get; set; }

        public ConnectionHandler(IConfiguration configuration)
        {
            Padrao = configuration.GetConnectionString("FootUnity");
        }

        NpgsqlConnection connection;

        string IDbConnection.ConnectionString { get; set; }

        int IDbConnection.ConnectionTimeout => 80000;

        string IDbConnection.Database => string.Empty;

        ConnectionState IDbConnection.State => connection.State;

        IDbTransaction IDbConnection.BeginTransaction()
        {
            throw new NotImplementedException();
        }

        IDbTransaction IDbConnection.BeginTransaction(IsolationLevel il)
        {
            throw new NotImplementedException();
        }

        void IDbConnection.ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        void IDbConnection.Close()
        {
            throw new NotImplementedException();
        }

        IDbCommand IDbConnection.CreateCommand()
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            if(connection != null)
                if (connection.State == ConnectionState.Open)
                    connection.Close();
        }

        void IDbConnection.Open()
        {
            connection.Open();
        }

        public NpgsqlConnection Create(string connectionString = null) 
        {
            if(connectionString != null)
                this.Padrao = connectionString;

            try
            {
                connection = new NpgsqlConnection(Padrao);
                connection.Open();
            }
            catch(Exception ex)
            {

            }

            return connection;
        }
    }
}
