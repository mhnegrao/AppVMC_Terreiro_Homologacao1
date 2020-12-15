using PwaTVMC.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PwaTVMC.Server.Services
{
    public class MensalidadeService : ServiceBase<Mensalidade>, IMensalidadeService
    {


        //public async Task<List<Mensalidade>> GetMensalidades()
        //{
        //    using var db = new LiteDatabase(Startup._database);
        //    var col = db.GetCollection<Mensalidade>("mensalidade");
        //    var query = await Task.FromResult(col.Include(c => c.NomeAfiliado).FindAll().ToList());
        //    return query;
        //}

        //public async Task<List<Mensalidade>> GetMensalidadeWithName(string texto)
        //{
        //    using var db = new LiteDatabase(Startup._database);
        //    var col = db.GetCollection<Mensalidade>("mensalidade");
        //    var query = await Task.FromResult(col.Include(c => c.NomeAfiliado).Find(f => f.NomeAfiliado.Contains(texto)).ToList());

        //    return query;
        //}
       

        public Task<List<Mensalidade>> GetMensalidadeWithName(string texto)
        {
            throw new NotImplementedException();
        }

       
    }
}