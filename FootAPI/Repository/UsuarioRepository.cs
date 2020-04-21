using Dapper;
using FootAPI.Interfaces;
using GeneralLibrary;
using System;
using System.Threading.Tasks;

namespace FootAPI.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConnectionHandler connection;

        public UsuarioRepository(IConnectionHandler connection) => this.connection = connection;

        public async Task<Tuple<bool,int>> ObterInfantilTamanho(int idUsuario)
        {
            using(var conexao = connection.Create())
            {
                var retorno = await conexao.QueryFirstOrDefaultAsync<dynamic>("SELECT infantil, idade FROM usuario WHERE id = @idUsuario", new { idUsuario });

                return Tuple.Create<bool, int>(retorno.infantil, retorno.idade);
            }
        }
    }
}
