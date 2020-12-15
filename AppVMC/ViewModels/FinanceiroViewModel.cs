using AppVMC.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppVMC.ViewModels
{
    public class FinanceiroViewModel : ViewModelBase
    {
        protected class MensalidadeResumo
        {
            public string MesRef;
            public string Afiliado;
            public int QtdeParcelas;
            public int QtdeParcelasPagas;
            public int QtdeParcelasNaoPagas;
            public decimal ValorMensalidade;
            public DateTime DataVencimento;
            public decimal ValorPago;
            public bool Pago;
            public DateTime DataPagamento;
            public decimal ValorRecebido { get; internal set; }
            public decimal ValorNaoRecebido { get; internal set; }
        }

        [Inject] protected IMensalidadeService ServiceFinanceiro { get; set; }
        protected List<MensalidadeResumo> resumoList = new List<MensalidadeResumo>();
        protected List<MensalidadeResumo> resumoAfiliadoList = new List<MensalidadeResumo>();

        protected override async Task OnInitializedAsync() => await GerarResumoMensalidades();

        private async Task GerarResumoMensalidades()
        {
            var result = await ServiceFinanceiro.GetAll();
            //define o resumo financeiro

            var qtdeMensalidades = result.Count();
            var valorTotal = result.Sum(s => s.ValorMensalidade);
            var gm = result
            .Where(w => !string.IsNullOrEmpty(w.NomeAfiliado)
            && !string.IsNullOrEmpty(w.MensalidadeReferencia)
           && w.ValorMensalidade > 0 && !w.Cancelado)
            .GroupBy(g => new
            {
                g.Referencia
            }).Select(s => new
            {
                MesRef = s.Key.Referencia,
                ValorMes = s.Sum(sm => sm.ValorMensalidade),
                QtdeMensalidades = s.Count(),
                ValorRecebido = s.Where(w => w.Pago).Sum(sm => sm.ValorMensalidade),
                QtdeMensalidadesPagas = s.Where(w => w.Pago).Count(),
                ValorNaoRecebido = s.Where(w => !w.Pago).Sum(sm => sm.ValorMensalidade),
                QtdeMensalidadesNaoPagas = s.Where(w => !w.Pago).Count(),
                OrderRef = s.Distinct().Select(s => s.MensalidadeReferencia).FirstOrDefault()
            }).OrderBy(o => o.MesRef).ToList();

            resumoList.Clear();
            foreach (var item in gm)
            {
                resumoList.Add(new MensalidadeResumo
                {
                    MesRef = item.OrderRef,
                    ValorMensalidade = item.ValorMes,
                    ValorRecebido = item.ValorRecebido,
                    ValorNaoRecebido = item.ValorNaoRecebido,
                    QtdeParcelas = item.QtdeMensalidades,
                    QtdeParcelasPagas = item.QtdeMensalidadesPagas,
                    QtdeParcelasNaoPagas = item.QtdeMensalidadesNaoPagas
                });
            }
            StateHasChanged();
        }

        protected async Task ExibirCaixa()
        {
            await Task.Run(() => OpenPage("/lancamentoindex"));
        }
    }
}