using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppVMC.Data;
using AppVMC.Models;
using LiteDB;

namespace AppVMC.Services
{
    public class MensalidadeService : ServiceBase<Mensalidade>, IMensalidadeService
    {
        public MensalidadeService(DatabaseContext dbService) : base(dbService)
        {
        }

        public async Task<List<Mensalidade>> GetMensalidades()
        {
            var query =await Task.FromResult(_dbService.Mensalidades.Select(s => s).ToList());
            return query;
        }

        public async  Task<List<Mensalidade>> GetMensalidadeWithName(string texto)
        {
            var query = await Task.FromResult(_dbService.Mensalidades.Where(f=>f.NomeAfiliado.Contains(texto)).ToList());
            return query;
        }

        #region...LiteDB...
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
        #endregion
    }
}