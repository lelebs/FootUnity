using System;
using System.Threading.Tasks;

namespace FootAPI.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Tuple<bool, int>> ObterInfantilTamanho(int idUsuario);
    }
}
