using System.Collections.Generic;

namespace PwaTVMC.Shared.Models
{
    public class Afiliado : Cliente
    {
        public Afiliado()
        {
            //BsonMapper.Global.Entity<Afiliado>().DbRef<List<Mensalidade>>(e => e.Mensalidades);
        }

        public bool IsMensalista { get; set; }
        public List<Mensalidade> Mensalidades { get; set; } = new List<Mensalidade>();
    }
}