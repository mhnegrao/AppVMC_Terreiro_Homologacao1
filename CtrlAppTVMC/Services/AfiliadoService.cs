using AppVMC.Data;
using AppVMC.Models;
using LiteDB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppVMC.Services
{
    public class AfiliadoService : ServiceBase<Afiliado>, IAfiliadoService
    {
        public AfiliadoService(DatabaseContext dbService) : base(dbService)
        {
        }

        public async Task<List<Afiliado>> GetAfiliados()
        {
            var result = await Task.FromResult(_dbService.Afiliados.Select(s => s).ToList());

            return result;
        }

        #region...LiteDB...
        //public async Task<List<Afiliado>> GetAfiliados()
        //{
        //    var mapper = LiteDB.BsonMapper.Global;

        // mapper.Entity<Mensalidade>().Id(e => e.Id).DbRef(x => x.IdFiliado, "mensalidades");

        // using var db = new LiteDatabase(Startup._database);

        // var col = db.GetCollection<Afiliado>("afiliado");

        // //var query = col.Include<Mensalidade>(e=>e.Mensalidades.);

        //    return col.FindAll().ToList();
        //}
        #endregion
    }
}