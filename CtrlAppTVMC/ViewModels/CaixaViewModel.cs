using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AppVMC.Models;
using AppVMC.Pages;
using AppVMC.Services;
using Microsoft.AspNetCore.Components;

namespace AppVMC.ViewModels {
    public class CaixaViewModel : ViewModelBase {
        [Inject] protected ICaixaService ServiceCaixa { get; set; }
        protected List<Caixa> caixaList = new List<Caixa> ();
        protected List<LancamentoCaixa> lanctoCaixaList = new List<LancamentoCaixa> ();
        protected static Caixa esteCaixa = new Caixa ();
        protected static LancamentoCaixa esteLancto = new LancamentoCaixa ();

        private async Task LancamentoNovo () {
            await Task.FromResult (NovoLancamento ());
        }

        protected Task NovoLancamento () {
            isEditing = false;
            esteLancto.IdMovimento = esteCaixa.Id;
            return Task.FromResult (Dialog.Show<CaixaNovoLancto> ("Novo Lançamento"));
        }

        protected async Task AddOrUpdateLancamento () {
            if (isEditing) {
                await ServiceCaixa.UpdateLancamento (esteLancto);
            } else {
                await ServiceCaixa.AddLancamento (esteLancto);
            }
            StateHasChanged ();
        }

        protected async Task DeleteLancamento (int id) {
            await ServiceCaixa.RemoveLancamento (id);
        }

        protected async Task AdicionarLancamento () {
            esteLancto.IdMovimento = esteCaixa.Id;
            await AddOrUpdateLancamento ();
            esteCaixa.Lancamentos.Add (esteLancto);
            await ServiceCaixa.Add (esteCaixa);
            await GoToIndex ();
        }

        private async Task GoToIndex () {
            isEditing = false;
            GetNewLancamento ();
            await MessageBox ("Inclusão de Lançamento", "Registro Incluido com Sucesso!!!", "success", "Ok", string.Empty, false);

            OpenPage ("/caixaindex");
        }

        protected async Task EditarLancamento () {
            await AddOrUpdateLancamento ();
            await MessageBox ("Edição de Lançamento", "Registro Editado com Sucesso!!!", "success", "Ok", string.Empty, false);
            await GoToIndex ();
        }

        private static void GetNewLancamento () {
            LancamentoCaixa obj = default;
            obj = Activator.CreateInstance<LancamentoCaixa> ();
            esteLancto = obj;
        }

        protected async Task ExcluirLancamento (int id) {
            await DeleteLancamento (id);
            await MessageBox ("Exclusão de Lançamento", "Registro Excluído com Sucesso!!!", "success", "Ok", string.Empty, false);
            await GoToIndex ();
        }

        protected async Task AbrirCaixa () {
            //verifica se caixa foi aberto no dia
            var isOpen = ServiceCaixa.CaixaAberto ().Result;
            if (!isOpen) {
                esteCaixa.SaldoAnterior = await Task.FromResult (ServiceCaixa.SaldoAnterior ()).Result;
                esteCaixa.DataMovimento = DateTime.Now;
                await Task.FromResult (ServiceCaixa.Add (esteCaixa));
            } else {
                esteCaixa = await Task.FromResult (ServiceCaixa.MovimentoDeHoje ()).Result;
            }
            StateHasChanged();
        }

        protected async Task FecharCaixa () {
            //verifica se caixa foi aberto no dia
            var isOpen = ServiceCaixa.CaixaAberto ().Result;
            if (!isOpen) {
                esteCaixa.SaldoAnterior = await Task.FromResult (ServiceCaixa.SaldoAnterior ()).Result;
                esteCaixa.DataMovimento = DateTime.Now;

                await Task.FromResult (ServiceCaixa.Add (esteCaixa));
            } else {
                esteCaixa = await Task.FromResult (ServiceCaixa.MovimentoDeHoje ()).Result;
            }
        }
    }
}