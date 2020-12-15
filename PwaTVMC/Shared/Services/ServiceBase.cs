using Microsoft.EntityFrameworkCore;
using PwaTVMC.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PwaTVMC.Server.Services
{
    public class ServiceBase<T> where T : class, IServiceBase<T>, new()
    {
        protected readonly DatabaseContext _dbService;

        public ServiceBase(DatabaseContext dbService)
        {
            _dbService = dbService;
        }

        public async Task Add(T item)
        {
            await _dbService.Set<T>().AddAsync(item);
            await _dbService.SaveChangesAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<T>> GetAll(int range = 10)
        {
            var result = await _dbService.Set<T>().AsNoTracking().ToListAsync();
            return result.Take(range).ToList();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbService.Set<T>().FindAsync(id);
        }

        public Task Remove(int id)
        {
            var item = _dbService.Set<T>().Find(id);
            if (item != null)
            {
                _dbService.Set<T>().Remove(item);
                return _dbService.SaveChangesAsync();
            }
            return null;
        }

        public Task Update(T item)
        {
            _dbService.Entry(item).State = EntityState.Modified;
            return _dbService.SaveChangesAsync();
        }

        public Task<T> GetNew()
        {
            var obj = new T();
            return Task.FromResult(obj);
        }
    }
}
