using Dapper;
using GeneralLibrary;
using IdentityAPI.Interfaces;
using IdentityAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IConnectionHandler connection;

        public LoginRepository(IConnectionHandler connectionHandler) => this.connection = connectionHandler;

        public async Task<Usuario> Obter(AuthModel model)
        {
            using(var conexao = connection.Create())
            {
                var query = "SELECT * FROM usuarios WHERE email = @Email AND senha = @Password";

                var aa = await conexao.QueryFirstOrDefaultAsync<Usuario>(query, model);

                return aa;
            }
        }
    }
}
