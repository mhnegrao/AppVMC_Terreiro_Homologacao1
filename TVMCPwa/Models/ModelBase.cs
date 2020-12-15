using System;

namespace TVMCPwa.Models
{
    public class ModelBase
    {

        public int Id { get; set; }
        public DateTime DataInclusao { get; set; } = DateTime.Now;
        public DateTime DataAlteracao { get; set; }
        public bool Ativo { get; set; }
        public bool Cancelado { get; set; }
    }
}
