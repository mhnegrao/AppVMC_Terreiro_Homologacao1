using AppVMC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVMC.Services
{
    public interface ILancamentoService:IServiceBase<LancamentoCaixa>
    {
        Task<List<LancamentoCaixa>> GetLancamentoByInterval(DateTime dataInicio, DateTime dataTermino);
    }
}
