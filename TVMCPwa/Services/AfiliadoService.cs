
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVMCPwa.Models;

namespace TVMCPwa.Services
{
    public class AfiliadoService : ServiceBase<Afiliado>, IAfiliadoService
    {
        //public async Task<List<Afiliado>>GetAfiliados(){
        //     var mapper = LiteDB.BsonMapper.Global;

        //    mapper.Entity<Mensalidade> ().Id(e=>e.Id).DbRef (x => x.IdFiliado,"mensalidades");

        //    using var db = new LiteDatabase (Startup._database);

        //    var col = db.GetCollection<Afiliado> ("afiliado");

        //    //var query = col.Include<Mensalidade>(e=>e.Mensalidades.);

        //   return col.FindAll().ToList();
        //}
    }
}
