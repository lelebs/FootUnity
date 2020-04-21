using System.Collections.Generic;

namespace FootAPI.Model
{
    public class Produto
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public IList<ProdutoTamanho> ListaDeTamanhos { get; set; }
        public byte[] Imagem { get; set; } 
    }
}