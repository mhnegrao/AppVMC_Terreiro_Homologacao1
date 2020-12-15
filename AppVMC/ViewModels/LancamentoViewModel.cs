using AppVMC.Models;
using AppVMC.Services;
using AppVMC.Pages;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using System.Runtime.CompilerServices;
using MatBlazor;

namespace AppVMC.ViewModels
{
    public class LancamentoViewModel : ViewModelBase
    {
        [Inject] protected ILancamentoService ServiceLancamento { get; set; }
        protected List<LancamentoCaixa> lanctoList = new List<LancamentoCaixa>();
        protected static LancamentoCaixa esteLancto = new LancamentoCaixa();
        protected int qtdeDebito;
        protected decimal valorDebito = 0;
        protected int qtdeCredito;
        protected decimal valorCredito = 0;
        protected override async Task OnInitializedAsync()
        {

            await ObterLancamentos();
            ObterTotais();

            StateHasChanged();
        }

        protected void ObterTotais()
        {
            var listDebito = lanctoList.Where(w => w.TipoLancto == 1).ToList();

            qtdeDebito = listDebito.Count;
            valorDebito = listDebito.Sum(s => s.ValorLancto);

            var listCredito = lanctoList.Where(w => w.TipoLancto == 2).ToList();
            qtdeCredito = listCredito.Count;
            valorCredito = listCredito.Sum(s => s.ValorLancto);
        }

        protected async Task ObterLancamentos()
        {
            var lista = await Task.FromResult(ServiceLancamento.GetAll()).Result;
            lanctoList.Clear();
            lanctoList.AddRange(lista);

        }

        protected async Task NovoLancamento()
        {
            isEditing = false;

            await Task.Run(() => Dialog.Show<LancamentoEdit>("Novo Lançamento"));
        }
        protected async Task ExcluirClick(int id)

        {
            var result = lanctoList.Find(f => f.Id == id);
            var mensagem = $"Confirma a exclusão do lançamento {result.DescricaoLancamento}?";
            var confirmado = await MessageBox($"Exclusão de Lançamento", $"{mensagem}", "question", "Sim", "Não");

            if (confirmado)
            {

                await Task.Run(() => ExcluirLancamento(id));

            }

        }
        protected async Task AddOrUpdateLancamento()
        {
            if (isEditing)
            {
                esteLancto.DataAlteracao = DateTime.Now;
                await ServiceLancamento.Update(esteLancto);
            }
            else
            {
                esteLancto.DataLancamento = DateTime.Now;
                await ServiceLancamento.Add(esteLancto);
            }
            StateHasChanged();
        }

        private async Task GoToIndex(bool forceload = false)
        {
            isEditing = false;
            GetNewLancamento();
            await Task.Run(() => OpenPage("/lancamentoindex", forceload));
            ObterTotais();
        }

        protected async Task EditarLancamento(int id)
        {
            isEditing = true;
            esteLancto = lanctoList.Find(f => f.Id == id);
            await Task.Run(()=>Dialog.Show<LancamentoEdit>("Editar Lançamento"));
        }

        protected async Task SalvarLancamento()
        {
            var titulo = isEditing ? "Alteração de Lançamento" : "Inclusão de Lançamento";
            var mensagem = isEditing ? "Lançamento alterado com sucesso!!!" : "Lançamento incluído com sucesso!!!";
            await AddOrUpdateLancamento();
            //await MessageBox(titulo, mensagem, "success", "Ok", string.Empty, false);
            ShowToast(titulo, mensagem, MatToastType.Success);
            await BlazoredModal.Close();
            await GoToIndex(true);
        }

        protected async Task FiltrarLancamentoPorPeriodo(DateTime dataInicio, DateTime dataTermino)
        {
            var result = await Task.FromResult(ServiceLancamento.GetLancamentoByInterval(dataInicio, dataTermino)).Result;
            lanctoList.Clear();
            lanctoList.AddRange(result);
            ObterTotais();
            StateHasChanged();
        }
        private static void GetNewLancamento()
        {
            LancamentoCaixa obj = Activator.CreateInstance<LancamentoCaixa>();
            esteLancto = obj;
        }

        protected async Task ExcluirLancamento(int id)
        {
            await ServiceLancamento.Remove(id);
            await MessageBox("Exclusão de Lançamento", "Lançamento Excluído com Sucesso!!!", "success", "Ok", string.Empty, false);
            await GoToIndex(true);
        }
    }
}