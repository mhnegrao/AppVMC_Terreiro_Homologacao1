namespace AppVMC.Models
{
    public class Endereco:ModelBase
    {
        public int IdCliente { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Municipio { get; set; }
        public string Bairro { get; set; }
        public string Uf { get; set; }
        public string Gps { get; set; }
    }
}