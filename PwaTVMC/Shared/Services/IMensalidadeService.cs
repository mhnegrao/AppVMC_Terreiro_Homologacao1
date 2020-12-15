using PwaTVMC.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PwaTVMC.Server.Services
{
    public interface IMensalidadeService : IServiceBase<Mensalidade>
    {
        Task<List<Mensalidade>> GetMensalidades();
        Task<List<Mensalidade>> GetMensalidadeWithName(string texto);
    }
}