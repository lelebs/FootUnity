using FootAPI.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootAPI.Interfaces
{
    public interface ILeitorRepository
    {
        Task<Leitor> ObterLeitor(string macAddress);
        Task<int> Inserir(Leitor leitor);
        Task Alterar(Leitor leitor);
        Task<Tuple<IEnumerable<Leitor>, int>> ObterLeitores();
    }
}
