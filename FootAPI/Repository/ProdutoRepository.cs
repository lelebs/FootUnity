using Dapper;
using FootAPI.Interfaces;
using FootAPI.Model;
using GeneralLibrary;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace FootAPI.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IConnectionHandler connection;

        public ProdutoRepository(IConnectionHandler connection) => this.connection = connection;

        public async Task<Produto> GetProduct(int id)
        {
            using(var conexao = connection.Create())
            {
                var consulta = @"SELECT produtos.* 
                                 FROM produtos
                                 WHERE produtos.id = @id";

                var consultaTamanhos = @"SELECT produtostamanho.* FROM produtostamanho WHERE idproduto = @id";

                var produto = await conexao.QueryFirstOrDefaultAsync<Produto>(consulta, new { id });
                var tamanhos = await conexao.QueryAsync<ProdutoTamanho>(consultaTamanhos, new { id });
                produto.ListaDeTamanhos = tamanhos.ToList();

                return produto;
            }
        }

        public async Task<IEnumerable<Produto>> GetProducts()
        {
            using (var conexao = connection.Create())
            {
                var consulta = "SELECT * FROM produtos";

                return await conexao.QueryAsync<Produto>(consulta);
            }
        }

        public async Task<IEnumerable<Produto>> GetProducts(double? size)
        {
            using (var conexao = connection.Create())
            {
                var consulta = @$"SELECT produto.*, array_agg(productssize.size) AS listaTamanhos
                                 FROM produto
                                 LEFT JOIN produtotamanho ON produtotamanho.idproduto = produto.id
                                 {(size.HasValue ? "WHERE produtotamanho.centimetros = @centimetros" : string.Empty)}
                                 GROUP BY produto.id, produto.titulo, produto.descricao, produto.imagem, produto.valor";

                return await conexao.QueryAsync<Produto>(consulta);
            }
        }

        public async Task<int> InsertProduct(Produto model)
        {
            using (var conexao = connection.Create())
            {
                return await conexao.QueryFirstOrDefaultAsync<int>("INSERT INTO produto(titulo,descricao,imagem,valor) VALUES (@Titulo, @Descricao, @Valor, @Imagem) RETURNING id", model);
            }
        }

        public async Task UpdateProduct(Produto model)
        {
            using (var conexao = connection.Create())
            {
                var query = @"UPDATE produto 
                          SET titulo = @Titulo,
                              descricao = @Descricao,
                              valor = @Valor,
                              imagem = @Imagem
                          WHERE id = @Id";

                await conexao.ExecuteAsync(query, model);
            }
        }
    }
}
