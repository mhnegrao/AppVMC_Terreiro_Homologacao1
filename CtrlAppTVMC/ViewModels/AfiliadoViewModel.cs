using AppVMC.Models;
using AppVMC.Pages;
using AppVMC.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppVMC.ViewModels
{
    public class AfiliadoViewModel : ViewModelBase
    {
        protected static Afiliado esteAfiliado = new Afiliado();
        [Inject] protected IMensalidadeService ServiceMensalidade { get; set; }
        protected static Mensalidade estaMensalidade = new Mensalidade();
        protected List<Afiliado> afiliadoList = new List<Afiliado>();
        [Inject] protected IAfiliadoService ServiceAfiliado { get; set; }

        protected override async Task OnInitializedAsync() => await ObterAfiliados();

        protected async Task NovoAfiliado()
        {
            esteAfiliado = ServiceAfiliado.GetNew();
            esteAfiliado.Id = afiliadoList.Count == 0 ? 1 : afiliadoList.Count + 1;
            var modal = await Task.FromResult(Dialog.Show<AfiliadoNovo>("Novo Afiliado"));
        }

        protected async Task NovaMensalidade()
        {
            estaMensalidade.IdFiliado = esteAfiliado.Id;
            estaMensalidade.Id = esteAfiliado.Mensalidades.Count == 0 ? 1 : esteAfiliado.Mensalidades.Count + 1;
            var modal = await Task.FromResult(Dialog.Show<GerarMesalidade>("Nova Mensalidade"));
        }

        protected async Task ObterAfiliados()
        {
            afiliadoList.Clear();
            var lista = await ServiceAfiliado.GetAll();
            foreach (var item in lista)
            {
                var result =await Task.FromResult(ServiceMensalidade.GetMensalidades()).Result;
                var mensalidades = result.Where(w => w.IdFiliado == item.Id).ToList();
                item.Mensalidades.AddRange(mensalidades);

            }
            afiliadoList.AddRange(lista);
        }

        protected async Task SalvarEdicao()
        {
            var titulo = isEditing ? "Atualização de  Afiliado" : "Inclusão de Afiliado";
            await AddOrUpdateAfiliado(esteAfiliado);
            await MessageBox(titulo, "Registro Gravado com Sucesso!!!", "success", "Ok", string.Empty, false);

            SetNew();
            StateHasChanged();
            OpenPage("/afiliadoindex");
        }

        protected async Task EditarAfiliado(int id)
        {
            isEditing = true;
            esteAfiliado = afiliadoList.Find(f => f.Id == id);
            OpenPage("/afiliadoedit");
        }

        protected async Task ExcluirAfiliado(int id)
        {
            esteAfiliado = await ServiceAfiliado.GetById(id);
            var confirmado = await MessageBox($"Exclusão de Registro ID {esteAfiliado.Id}", $"ALERTA!!!{Environment.NewLine}Confirma a exclusão do afiliado: {esteAfiliado.Nome}?", "warning", "Sim", "Não");
            if (confirmado)
            {
                await RemoveAfiliado(id);
                await MessageBox("Exclusão de Registro", "Registro Excluído com Sucesso!!!", "success", "Ok", string.Empty, false);

                SetNew();
                await ObterAfiliados();
                StateHasChanged();
            }
        }

        private async Task RemoveAfiliado(int id)
        {
            if (id > 0)
            {
                await ServiceAfiliado.Remove(id);
            }
        }

        private async Task AddOrUpdateAfiliado(Afiliado item)
        {
            if (!isEditing)
            {
                await ServiceAfiliado.Add(item);
            }
            else
            {
                item.DataAlteracao = DateTime.Now;
                await ServiceAfiliado.Update(item);
            }
        }

        protected async Task EditarMensalidade(int id)
        {
        }

        protected async Task ExcluirMensalidade(int id)
        {
        }

        private void SetNew()
        {
            wasChanged = false;
            isEditing = false;
            esteAfiliado = ServiceAfiliado.GetNew();
        }
    }
}