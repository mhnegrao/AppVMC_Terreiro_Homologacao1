using AppVMC.Data;
using AppVMC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVMC.Services
{
    public class CaixaService : ServiceBase<Caixa>, ICaixaService
    {
        public CaixaService(DatabaseContext dbService) : base(dbService)
        {
        }

        public async Task<bool> CaixaAberto()
        {
                var result = await Task.FromResult(_dbService.Caixa.Where(w => w.DataMovimento.Date == DateTime.Now.Date).Count());
                return result > 0 ? true : false;
              }

        public async Task UpdateLancamento(LancamentoCaixa item)
        {
            await Task.FromResult(_dbService.LancamentosCaixa.Update(item));
            await Task.FromResult(_dbService.SaveChanges());
        }

        public async Task RemoveLancamento(int id)
        {

            {
                var item = await Task.FromResult(_dbService.LancamentosCaixa.FirstOrDefault(f => f.Id == id));
                await Task.FromResult(_dbService.LancamentosCaixa.Remove(item));
                await Task.FromResult(_dbService.SaveChanges());
            }
        }

        public async Task AddLancamento(LancamentoCaixa lancto)
        {
            await Task.FromResult(_dbService.LancamentosCaixa.Add(lancto));
            await Task.FromResult(_dbService.SaveChanges());
        }

        public async Task<Caixa> MovimentoDeHoje()
        {
            var result = await Task.FromResult(_dbService.Caixa
                .Where(w => w.DataMovimento.Date.Equals(DateTime.Now.Date))
                .FirstOrDefault());
            return result;
        }

        public async Task<List<Caixa>> MovimentoDoPeriodo(DateTime dataInicio, DateTime dataTerminio)
        {
            var result = await Task.FromResult(_dbService.Caixa.Where(w => w.DataMovimento.Date >= dataInicio.Date && w.DataMovimento.Date <= dataTerminio.Date).OrderByDescending(o => o.DataMovimento).ToList());
            return result;
        }

        public async Task<LancamentoCaixa> GetLancamentoById(int id)
        {

            var lista = await Task.FromResult(_dbService.LancamentosCaixa.FirstOrDefault(f => f.Id == id));
            return lista;
        }

        public async Task<List<LancamentoCaixa>> GetLancamentoByMovimento(int idMovimento)
        {
            var result = GetById(idMovimento).Result;

            var lista = await Task.FromResult(result.Lancamentos.Where(f => f.IdMovimento == idMovimento).ToList());
            return lista;
        }

        public async Task<decimal> SaldoAnterior()
        {
            var saldo = 0m;
            var result = GetAll().Result;

             saldo = await Task.FromResult(result
                .Where(w => w.DataMovimento.Date.Equals(DateTime.Now.Date))
                .OrderByDescending(o => o.DataMovimento)
                .FirstOrDefault().SaldoAtual);
            return saldo;
        }
    }
}
