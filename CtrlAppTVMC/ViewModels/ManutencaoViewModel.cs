using AppVMC.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVMC.ViewModels
{
    public class ManutencaoViewModel : ViewModelBase
    {
        protected async Task GerarBackup()
        {
            var pathfile = Startup._database;
            //await JSRuntime.InvokeVoidAsync("downloadfile", pathfile);
            await JSRuntime.InvokeVoidAsync(
     "downloadFromUrl",
     new
     {
         Url = "api/pictures/1",
         FileName = pathfile
     });
        }
    }
}