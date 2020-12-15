using System;
using System.Collections.Generic;

namespace AppVMC.Models
{
    public class Caixa:ModelBase
    {
        public DateTime DataMovimento { get; set; }
        public decimal SaldoAtual { get; set; }
        public decimal SaldoAnterior { get; set; }
        public List<LancamentoCaixa> Lancamentos { get; set; }
        public string Observacao { get; set; }
    }
}
