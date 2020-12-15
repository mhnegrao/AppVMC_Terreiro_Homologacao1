using System;

namespace AppTVMC.Models
{
    public class Mensalidade : ModelBase
    {
        public int IdFiliado { get; set; }

        public string MensalidadeReferencia
        {
            get
            {
                var refMes = DataVencimento.ToString("MMMM/yyyy").ToUpper();
                return refMes;
            }
        }

        public string Referencia
        {
            get
            {
                var refMes = DataVencimento.ToString("yyyy/MM").ToUpper();
                return refMes;
            }
        }

        public decimal ValorMensalidade { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public bool Pago { get; set; }
        public decimal ValorPago { get; set; }
        public string NomeAfiliado { get; set; }
    }
}