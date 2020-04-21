using IdentityAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Interfaces
{
    public interface ILoginRepository
    {
        Task<Usuario> Obter(AuthModel model);
    }
}
