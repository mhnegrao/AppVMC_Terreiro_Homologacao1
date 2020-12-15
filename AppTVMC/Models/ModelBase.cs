using System;

namespace AppTVMC.Models
{
    public class ModelBase
    {
        public int Id { get; set; }
        public DateTime DataInclusao { get; set; } = DateTime.Now;
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; } = true;
        public bool Cancelado { get; set; }
    }
}
