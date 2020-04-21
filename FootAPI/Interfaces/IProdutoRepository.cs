using FootAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootAPI.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetProducts();

        Task<IEnumerable<Produto>> GetProducts(double? size);

        Task<Produto> GetProduct(int id);

        Task<int> InsertProduct(Produto model);

        Task UpdateProduct(Produto model);
    }
}