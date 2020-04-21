using Dapper;
using FootAPI.Interfaces;
using FootAPI.Model;
using GeneralLibrary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootAPI.Repository
{
    public class LeitorRepository : ILeitorRepository
    {
        private readonly IConnectionHandler connection;

        public LeitorRepository(IConnectionHandler connection) => this.connection = connection;

        public async Task Alterar(Leitor leitor)
        {
            using (var conexao = connection.Create())
            {
                var consulta = @"UPDATE leitor
                                 SET descricao = @Descricao, 
                                     macaddress = @MacAddress, 
                                     ativo = @Ativo
                                 WHERE id = @Id";

                await conexao.ExecuteAsync(consulta, leitor);
            }
        }

        public async Task<int> Inserir(Leitor leitor)
        {
            using (var conexao = connection.Create())
            {
                var consulta = @"INSERT INTO leitor(descricao, macaddress, ativo) 
                                 VALUES (@Descricao, @MacAddress, @Ativo) 
                                 RETURNING id";

                return await conexao.QueryFirstOrDefaultAsync<int>(consulta, leitor);
            }
        }

        public async Task<Leitor> ObterLeitor(string macAddress)
        {
            using(var conexao = connection.Create())
            {
                var consulta = "SELECT * FROM leitor WHERE macaddress = @macAddress";

                return await conexao.QueryFirstOrDefaultAsync<Leitor>(consulta, new { macAddress });
            }
        }

        public async Task<Tuple<IEnumerable<Leitor>, int>> ObterLeitores()
        {
            using (var conexao = connection.Create())
            {
                var consulta = "SELECT * FROM leitor WHERE ativo";

                var data = await conexao.QueryAsync<Leitor>(consulta);
                var count = await conexao.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM leitor WHERE ativo");

                return Tuple.Create(data, count);
            }
        }
    }
}
