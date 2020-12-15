using AppVMC.ViewModels;
using LiteDB;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVMC.Services
{
    public class ManutencaoService :ViewModelBase
    {
        public Task GerarXls()
        {
            throw new NotImplementedException();
        }

        public async Task LimparBaseDeDados()
        {
            using var db = new LiteDatabase(Startup._database);
            var cols = db.GetCollectionNames();
            foreach (var item in db.GetCollectionNames())
            {
                var col = db.GetCollection(item);
                await Task.FromResult(col.DeleteAll());
            }
        }


    }
}