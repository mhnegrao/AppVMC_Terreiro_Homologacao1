using AppVMC.Data;
using AppVMC.Models;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVMC.Services
{
    public class LancamentoService : ServiceBase<LancamentoCaixa>, ILancamentoService
    {
        public LancamentoService(DatabaseContext dbService) : base(dbService)
        {
        }

        public async Task<List<LancamentoCaixa>> GetLancamentoByInterval(DateTime dataInicio, DateTime dataTermino)
        {
            var query = await Task.FromResult(_dbService.LancamentosCaixa.Where(f => f.DataLancamento.Date>=dataInicio.Date && f.DataLancamento.Date<=dataTermino.Date).ToList());
            return query;
        }
    }
}
