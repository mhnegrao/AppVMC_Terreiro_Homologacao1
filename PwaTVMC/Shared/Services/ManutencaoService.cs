using System;
using System.Threading.Tasks;

namespace PwaTVMC.Server.Services
{
    public class ManutencaoService
    {
        public Task GerarXls()
        {
            throw new NotImplementedException();
        }

        //public async Task LimparBaseDeDados()
        //{
        //    using var db = new LiteDatabase(Startup._database);
        //    var cols = db.GetCollectionNames();
        //    foreach (var item in db.GetCollectionNames())
        //    {
        //        var col = db.GetCollection(item);
        //        await Task.FromResult(col.DeleteAll());
        //    }
        //}

        public async Task RestaurarBackup()
        {
            throw new NotImplementedException();
        }
    }
}