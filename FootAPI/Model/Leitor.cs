namespace FootAPI.Model
{
    public class Leitor
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string MacAddress { get; set; }
        public bool Ativo { get; set; } = true;
    }
}