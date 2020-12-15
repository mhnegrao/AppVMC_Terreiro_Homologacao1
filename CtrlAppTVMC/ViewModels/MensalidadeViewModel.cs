using AppVMC.Models;
using AppVMC.Pages;
using AppVMC.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVMC.ViewModels
{
    public class MensalidadeViewModel : ViewModelBase
    {
        protected enum TipoMensalidadeGerar
        {
            Individual = 1, Lote
        }

        protected static Mensalidade estaMensalidade = new Mensalidade();
        protected static List<Mensalidade> mensalidadeList = new List<Mensalidade>();
        protected static List<Mensalidade> mensalidades = new List<Mensalidade>();
        protected static List<Afiliado> afiliadoList = new List<Afiliado>();
        protected string textoProcura = string.Empty;
        [Inject] protected IMensalidadeService ServiceMensalidade { get; set; }
        [Inject] protected IAfiliadoService ServiceAfiliado { get; set; }

        protected List<string> mesRefList = new List<string>();
        protected override async Task OnInitializedAsync()
        {
            await ObterMensalidades();

            GetMesRef();

        }

        private void GetMesRef()
        {
            mesRefList.Clear();
            var result = mensalidades
                        .GroupBy(g => new
                        {
                            MesBase = g.MensalidadeReferencia,
                            Ref = g.Referencia
                        })
                        .OrderBy(o => o.Key.Ref)
                        .Select(s => s.Key.MesBase)
                        .ToList();

            mesRefList.AddRange(result);
        }

        protected async Task ObterMensalidades()
        {
            mensalidadeList.Clear();
            mensalidades.Clear();
            var lista = await ServiceMensalidade.GetMensalidades();
            mensalidades.AddRange(lista);
            mensalidadeList.AddRange(lista);
        }

        protected async Task BuscarMensalidadeContendoTexto(string texto)
        {
            mensalidadeList.Clear();
            var result = await ServiceMensalidade.GetMensalidadeWithName(texto);

            mensalidadeList.AddRange(result);
            StateHasChanged();

        }
        protected async Task ClearSearch()
        {
            textoProcura = string.Empty;
            await ObterMensalidades();
            StateHasChanged();
        }
        protected async Task GerarParcela(int idAfiliado, int qtdeParcelas, decimal valorParcela, int diaVencimento, bool emLote)
        {
            var vencimentoInicial = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, diaVencimento);
            //gera um lote de mensalidade para todos afiliados
            if (emLote)
            {
                var estaMensalidade = ServiceMensalidade.GetNew();
                var lista = ServiceAfiliado.GetAll().Result;
                afiliadoList.Clear();
                afiliadoList.AddRange(lista);

                mensalidadeList.Clear();
                //cria as parcelas

                foreach (var afiliado in afiliadoList)
                {
                    for (int i = 0; i < qtdeParcelas; i++)
                    {
                        mensalidadeList.Add(new Mensalidade
                        {
                            IdFiliado = afiliado.Id,
                            DataVencimento = vencimentoInicial,
                            ValorMensalidade = valorParcela,
                            NomeAfiliado = afiliado.Nome
                        });
                        vencimentoInicial = NextMonth(vencimentoInicial);
                        afiliado.Mensalidades.Add(mensalidadeList[i]);
                    }

                    await ServiceAfiliado.Update(afiliado);
                }
                foreach (var item in mensalidadeList)
                {
                    await ServiceMensalidade.Add(item);
                }
            }
            else
            {
                var esteAfiliado = ServiceAfiliado.GetById(idAfiliado).Result;

                for (int i = 0; i < qtdeParcelas; i++)
                {
                    estaMensalidade = new Mensalidade
                    {
                        IdFiliado = idAfiliado,
                        DataVencimento = vencimentoInicial,
                        ValorMensalidade = valorParcela,
                        NomeAfiliado = esteAfiliado.Nome
                    };
                    esteAfiliado.Mensalidades.Add(estaMensalidade);
                    await ServiceMensalidade.Add(estaMensalidade);

                    vencimentoInicial = NextMonth(vencimentoInicial);
                }
                await ServiceAfiliado.Update(esteAfiliado);
            }
        }

        protected async Task EditarMensalidade(int id)
        {
            isEditing = true;
            estaMensalidade = await Task.FromResult(mensalidadeList.Find(f => f.Id == id));
            OpenPage("/editmensalidade");
        }
        private async Task AddOrUpdateAfiliado(Mensalidade item)
        {
            if (!isEditing)
            {
                await ServiceMensalidade.Add(item);
            }
            else
            {
                item.DataAlteracao = DateTime.Now;
                await ServiceMensalidade.Update(item);
            }
        }
        protected async Task BaixarMensalidade(int id)
        {
            estaMensalidade = await ServiceMensalidade.GetById(id);
            var modal = await Task.FromResult(Dialog.Show<BaixaMensalidade>("Baixa de Mensalidade"));
        }

        protected async Task ExcluirMensalidade(int id)
        {
            estaMensalidade = await ServiceMensalidade.GetById(id);
            var conteudo = new StringBuilder();
            conteudo.Append($"Nome: {estaMensalidade.NomeAfiliado}");
            conteudo.Append(Environment.NewLine);
            conteudo.Append($"Parcela: {estaMensalidade.MensalidadeReferencia}");
            conteudo.Append(Environment.NewLine);
            conteudo.Append($"Valor: {estaMensalidade.ValorMensalidade.ToString("c")}");
            conteudo.Append(Environment.NewLine);
            conteudo.Append($"Vencto.: {estaMensalidade.DataVencimento.ToShortDateString()}");
            var confirmado = await MessageBox($"Exclusão de Mensalidade", conteudo.ToString(), "question", "Sim", "Não");
            if (confirmado)
            {
                await ServiceMensalidade.Remove(id);
                await MessageBox("Exclusão de Registro", "Mensalidade Excluída com Sucesso!!!", "success", "Ok", string.Empty, false);

                SetNew();
                GlobalState.InvokeStateChange();
                await ObterMensalidades();

            }
        }
        private void SetNew()
        {
            wasChanged = false;
            isEditing = false;
            estaMensalidade = ServiceMensalidade.GetNew();
        }
        public static DateTime NextMonth(DateTime date)
        {
            DateTime nextMonth = date.AddMonths(1);

            if (date.Day != DateTime.DaysInMonth(date.Year, date.Month)) //is last day in month
            {
                //any other day then last day
                return nextMonth;
            }
            else
            {
                //last day in the month will produce the last day in the next month
                return date.AddDays(DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month));
            }
        }
        protected async Task SalvarEdicao()
        {
            var titulo = isEditing ? "Atualização de  Mensalidade" : "Inclusão de Mensalidade";
            await AddOrUpdateAfiliado(estaMensalidade);
            await MessageBox(titulo, "Registro Gravado com Sucesso!!!", "success", "Ok", string.Empty, false);

            SetNew();
            StateHasChanged();
            OpenPage("/mensalidadeindex");
        }
        protected void GerarMensalidade()
        {
            isEditing = false;
            OpenPage("/gerarmensalidade");
        }
    }
}