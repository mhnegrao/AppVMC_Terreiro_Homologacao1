using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace TVMCPwa.ViewModels
{
    public class ManutencaoViewModel : ViewModelBase
    {
        protected async Task GerarBackup()
        {
            //var pathfile = Startup._database;
            //await JSRuntime.InvokeVoidAsync("downloadfile", pathfile);
            await JSRuntime.InvokeVoidAsync(
     "downloadFromUrl",
     new
     {
         Url = "api/pictures/1"
         //FileName = pathfile
     });
        }
    }
}