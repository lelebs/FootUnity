using Dapper;
using GeneralLibrary;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace FootAPI.Repository
{
    public class ErrorLoggingRepository
    {
        private readonly IConnectionHandler connection;

        public ErrorLoggingRepository(IConnectionHandler connection) => this.connection = connection;

        public async Task RegistrarErro(Exception ex)
        {
            using (var conexao = connection.Create())
            {
                var registro = JsonConvert.SerializeObject(ex);

                var query = "INSERT INTO erros(erro, datahora) VALUES(@registro, @dataHora)";

                await conexao.ExecuteAsync(query, new { registro, dataHora = DateTime.Now });
            }
        }
    }
}
