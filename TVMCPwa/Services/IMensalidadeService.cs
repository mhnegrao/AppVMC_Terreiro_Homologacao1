using System.Collections.Generic;
using System.Threading.Tasks;
using TVMCPwa.Models;

namespace TVMCPwa.Services
{
    public interface IMensalidadeService : IServiceBase<Mensalidade>
    {
        Task<List<Mensalidade>> GetMensalidades();
        Task<List<Mensalidade>> GetMensalidadeWithName(string texto);
    }
}