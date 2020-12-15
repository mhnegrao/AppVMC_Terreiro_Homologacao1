using System.Collections.Generic;

namespace AppVMC.Models
{
    public class Afiliado : Cliente
    {

        public bool IsMensalista { get; set; }
        public List<Mensalidade> Mensalidades { get; set; } = new List<Mensalidade>();
    }
}