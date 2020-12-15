using System;

namespace AppTVMC.Models
{
    public class LancamentoCaixa : ModelBase
    {
        public int IdMovimento { get; set; }
        public DateTime DataLancamento { get; set; }
        public int TipoLancto { get; set; }
        public decimal ValorLancto { get; set; }
        public string DescricaoLancamento { get; set; }

        public string TipoLanctoToString
        {
            get
            {
                var result = TipoLancto == 1 ? "D" : TipoLancto == 2 ? "C" : "E";
                return result;
            }
        }
    }
}
