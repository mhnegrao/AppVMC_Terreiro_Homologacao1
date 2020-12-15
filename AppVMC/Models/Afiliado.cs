using System.Collections.Generic;
using LiteDB;

namespace AppVMC.Models
{
    public class Afiliado : Cliente
    {
        public Afiliado()
        {
        }

        public bool IsMensalista { get; set; }
        public List<Mensalidade> Mensalidades { get; set; } = new List<Mensalidade>();
    }
}