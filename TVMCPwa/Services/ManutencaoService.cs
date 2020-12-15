using System;
using System.Threading.Tasks;
using TVMCPwa.ViewModels;

namespace TVMCPwa.Services
{
    public class ManutencaoService : ViewModelBase
    {
        public Task GerarXls()
        {
            throw new NotImplementedException();
        }

        public async Task LimparBaseDeDados()
        {
            //using var db = new LiteDatabase(Startup._database);
            //var cols = db.GetCollectionNames();
            //foreach (var item in db.GetCollectionNames())
            //{
            //    var col = db.GetCollection(item);
            //    await Task.FromResult(col.DeleteAll());
            //}
        }

        public async Task RestaurarBackup()
        {
            throw new NotImplementedException();
        }
    }
}