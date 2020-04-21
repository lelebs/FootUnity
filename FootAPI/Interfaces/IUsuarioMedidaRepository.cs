using FootAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootAPI.Interfaces
{
    public interface IUsuarioMedidaRepository
    {
        Task<int> InserirMedida(Measure model);
        Task<IList<Measure>> ObterMedidas(int idUsuario);
    }
}
