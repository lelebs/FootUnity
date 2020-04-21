using Dapper;
using FootAPI.Interfaces;
using FootAPI.Model;
using GeneralLibrary;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootAPI.Repository
{
    public class UsuarioMedidaRepository : IUsuarioMedidaRepository
    {
        private readonly IConnectionHandler connection;

        public UsuarioMedidaRepository(IConnectionHandler connection)
        {
            this.connection = connection;
        }

        public async Task<int> InserirMedida(Measure model)
        {
            using(var conexao = connection.Create())
            {
                var query = "INSERT INTO medida (idusuario, )";

                return await conexao.QueryFirstOrDefaultAsync<int>(query, model);
            }
        }

        public async Task<IList<Measure>> ObterMedidas(int idUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
