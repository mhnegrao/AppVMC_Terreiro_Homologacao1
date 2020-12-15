using AppVMC.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppVMC.Services
{
    public class ServiceBase<T> : IServiceBase<T> where T : class
    {
        protected readonly DatabaseContext _dbService;

        public ServiceBase(DatabaseContext dbService)
        {
            _dbService = dbService;
        }

        public async Task Add(T item)
        {

            {
                await _dbService.Set<T>().AddAsync(item);
                await _dbService.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<T>> GetAll()
        {

            {
                var result = await _dbService.Set<T>().AsNoTracking().ToListAsync();
                return result.ToList();
            }
        }

        public async Task<T> GetById(int id)
        {

            {
                return await _dbService.Set<T>().FindAsync(id);
            }
        }

        public Task Remove(int id)
        {

            {
                var item = _dbService.Set<T>().Find(id);
                if (item != null)
                {
                    _dbService.Set<T>().Remove(item);
                    return _dbService.SaveChangesAsync();
                }
                return Task.CompletedTask;
            }
        }

        public async Task Update(T item)
        {

            {
                _dbService.Entry(item).State = EntityState.Modified;
                await _dbService.SaveChangesAsync();
            }
        }

        public Task<T> GetNew()
        {
            T obj = default;
            obj = Activator.CreateInstance<T>();
            return Task.FromResult<T>(obj);
        }
    }
}