using System;

namespace FootAPI.Model
{
    public class ProdutoTamanho
    {
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public double Centimetros { get; set; }
        public double US { get; set; }
        public double UK { get; set; }
        public double EUR { get; set; }
        public double Brazil { get; set; }
    }
}
