using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTVMC.Models
{
    public class Caixa : ModelBase
    {
        public DateTime DataMovimento { get; set; }
        public decimal SaldoAtual { get; set; }
        public decimal SaldoAnterior { get; set; }
        public List<LancamentoCaixa> Lancamentos { get; set; }
        public string Observacao { get; set; }
    }
}
