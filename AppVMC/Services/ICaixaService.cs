using AppVMC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVMC.Services
{
    public interface ICaixaService:IServiceBase<Caixa>
    {
        Task<bool> CaixaAberto();
        Task<Caixa> MovimentoDeHoje();
        Task<List<Caixa>> MovimentoDoPeriodo(DateTime dataInicio, DateTime dataTerminio);
        Task<decimal> SaldoAnterior();
        Task AddLancamento(LancamentoCaixa lancto);
        Task RemoveLancamento(int id);
        Task UpdateLancamento(LancamentoCaixa item);
        Task <LancamentoCaixa>GetLancamentoById(int id);
        Task<List<LancamentoCaixa>> GetLancamentoByMovimento(int idMovimento);
    }
}
