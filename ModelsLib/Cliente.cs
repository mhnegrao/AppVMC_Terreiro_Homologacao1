namespace AppVMC.Models
{
    public class Cliente : ModelBase
    {
        public string Nome { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string LoginPassword { get; set; }
        public Endereco Endereco { get; set; }
        public string Observacao { get; set; }
    }
    
}