using Microsoft.AspNetCore.Components;
using System;

namespace AppVMC.Services
{
    public class ToastBase : ComponentBase, IDisposable
    {
        [Inject] private ToastService ToastService { get; set; }

        protected string Heading { get; set; }
        protected string Message { get; set; }
        protected bool IsVisible { get; set; }
        protected string BackgroundCssClass { get; set; }
        protected string IconCssClass { get; set; }

        protected override void OnInitialized()
        {
            ToastService.OnShow += ShowToast;
            ToastService.OnHide += HideToast;
        }

        private void ShowToast(string heading,string message, ToastLevel level)
        {
            BuildToastSettings(heading,level, message);
            IsVisible = true;
            StateHasChanged();
        }

        private void HideToast()
        {
            IsVisible = false;
            StateHasChanged();
        }

        private void BuildToastSettings(string heading,ToastLevel level, string message)
        {
            switch (level)
            {
                case ToastLevel.Info:
                    BackgroundCssClass = "bg-info";
                    IconCssClass = "info";
                    //Heading = "Info";
                    break;

                case ToastLevel.Success:
                    BackgroundCssClass = "bg-success";
                    IconCssClass = "check";
                   // Heading = "Success";
                    break;

                case ToastLevel.Warning:
                    BackgroundCssClass = "bg-warning";
                    IconCssClass = "exclamation";
                    //Heading = "Warning";
                    break;

                case ToastLevel.Error:
                    BackgroundCssClass = "bg-danger";
                    IconCssClass = "times";
                    //Heading = "Error";
                    break;
            }
            Heading = heading;
            Message = message;
        }

#pragma warning disable CA1816 // Os métodos Dispose devem chamar SuppressFinalize
        public void Dispose()
#pragma warning restore CA1816 // Os métodos Dispose devem chamar SuppressFinalize
        {
            ToastService.OnShow -= ShowToast;
        }
    }
}