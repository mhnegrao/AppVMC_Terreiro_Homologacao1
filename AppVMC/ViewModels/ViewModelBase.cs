using AppVMC.Services;
using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace AppVMC.ViewModels
{
    public class ViewModelBase : ComponentBase
    {
        public class DataChart
        {
            public string XLabel { get; set; }
            public string YValue { get; set; }
        }

        [Inject] protected SweetAlertService Swal { get; set; }
        [Inject] private NavigationManager UriHelper { get; set; }
        [Inject] protected IJSRuntime JSRuntime { get; set; }

        //[Inject] protected GraficoChartJS GraficosJS { get; set; }
        [Inject] protected IMatToaster Toaster { get; set; }

        [Inject] protected GlobalStateChange GlobalState { get; set; }
        [CascadingParameter] protected IModalService Dialog { get; set; }
        [CascadingParameter] protected BlazoredModalInstance BlazoredModal { get; set; }

//[Inject] protected ToastService ToastService { get; set; }
        protected static string Username { get; set; }

        protected static string Password { get; set; }
        protected ModalOptions dialogOptions = new ModalOptions();

        protected static bool isEditing = false;
        protected static bool wasChanged = false;

        protected string UrlInfo()
        {
            return $"Uri: {UriHelper.Uri} Url base: {UriHelper.BaseUri}";
        }
        protected void OpenPage(string page, bool forcedLoad = false)
        {

            var baseEnv = "/";
            if(Startup._envProdution)
            {
                baseEnv = Startup._domainPath;
            }
            Console.WriteLine(baseEnv);
            page = $"{baseEnv}{page.Replace("/", "")}";
            Console.WriteLine(page);
            UriHelper.NavigateTo(page, forcedLoad);


        }

        protected void ShowToast(string titulo, string mensagem, MatToastType type)
        {
            Toaster.Add(mensagem, type, titulo);
        }

        /// <summary>
        /// Exibe uma janela modal de confirmação ou informação
        /// </summary>
        /// <param name="titulo">Título da Messagebox</param>
        /// <param name="mensagem">mensagem do corpo da Messagebox</param>
        /// <param name="icone">Ícone utilizar: info,question,error ou warning</param>
        /// <param name="textoConfirma">texto do botão confirma</param>
        /// <param name="textoCancela">texto do botão cancela</param>
        /// <param name="isConfirmed">Se true exibe o botão cancelar</param>
        /// <returns></returns>
        protected async Task<bool> MessageBox(string titulo, string conteudo, string icone, string textoConfirma = "Confirma", string textoCancela = "Cancela", bool isConfirmed = true, bool isHtml = false)
        {


            bool choice = false;
            var myicone = (SweetAlertIcon)icone;
            if (!isConfirmed) textoConfirma = "Ok";

            var result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = titulo,
                Html = isHtml ? conteudo : null,
                Text = conteudo,
                AllowEnterKey = true,
                Icon = myicone,
                ShowCancelButton = isConfirmed,
                ConfirmButtonText = textoConfirma,
                CancelButtonText = textoCancela,
                CancelButtonColor = "error",
                AllowEscapeKey = true,
                AllowOutsideClick=false

            });
            if (result.Dismiss == DismissReason.Cancel)
            {
                await Swal.FireAsync(
                    "Oops!",
                    "Ação cancelada.",
                    SweetAlertIcon.Error
                );
            }
            else
            {
                choice = true;
            }
            return choice;
        }

        protected async Task<bool> MessageBoxModal(string titulo, string conteudo, string icone, string textoConfirma = "Confirma", string textoCancela = "Cancela", bool isConfirmed = true, bool isHtml = false)
        {
           // _ = BlazoredModal.Close().ConfigureAwait(false);

            bool choice = false;
            var myicone = (SweetAlertIcon)icone;
            if (!isConfirmed) textoConfirma = "Ok";

            await Swal.FireAsync(new SweetAlertOptions
            {
                Title = titulo,
                Html = isHtml ? conteudo : null,
                Text = conteudo,
                AllowEnterKey = true,
                Icon = myicone,
                ShowCancelButton = isConfirmed,
                ConfirmButtonText = textoConfirma,
                CancelButtonText = textoCancela,
                CancelButtonColor = "error",
                AllowEscapeKey = true
            }).ContinueWith(swalTask =>
           {
               SweetAlertResult result = swalTask.Result;

               if (result.Dismiss != DismissReason.Cancel)
               {
                   choice = true;
               }
               else if (result.Dismiss == DismissReason.Cancel)
               {
                   Swal.FireAsync(
                     "Oops!!!!!",
                     "Ação Cancelada!",
                     SweetAlertIcon.Error
                     );
               }
           });
            return choice;
        }

        protected async Task<bool> PopUp(string html, string titulo, string textButtonConfirm = "Sim", string textButtonCancel = "Não")
        {
            var result = await Swal.FireAsync(new SweetAlertOptions
            {
                Html = html,
                Title = titulo,
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true,
                ConfirmButtonText = textButtonConfirm,
                CancelButtonText = textButtonCancel
            }).ConfigureAwait(true);
            if (result.Dismiss == DismissReason.Cancel)
            {
                await Swal.FireAsync(
                    "Cancelado!",
                    "Ação Cancelada!",
                    SweetAlertIcon.Error
                );
            }

            return result.IsConfirmed;
        }

        //protected string Codificar(string texto)
        //{
        //    return Security.Encrypt(texto);
        //}
        //protected string Decodificar(string texto)
        //{
        //    return !string.IsNullOrEmpty(texto) ? Security.Decrypt(texto) : string.Empty;
        //}
        public async Task<bool> MakeLogin()
        {
            var list = await JSRuntime.InvokeAsync<string>("login");
            var inputs = JsonConvert.DeserializeObject<string[]>(list);
            Username = inputs[0];
            Password = inputs[1];
            //StateHasChanged();
            return LoginValido(Username, Password);
        }

        public bool LoginValido(string email, string senha)
        {
            var loginOk = false;

            if (senha == "1234" && email == "teste@teste.com")
                loginOk = true;

            return loginOk;
        }
    }

    public class GlobalStateChange
    {
        public void InvokeStateChange()
        {
            this.StateHasChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler StateHasChanged;
    }
}