using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVMCPwa.Services
{
    public interface IServiceBase<T> where T:class
    {
        Task<T> GetNew();

        Task Add(T item);

        Task<T> GetById(int id);

        Task<List<T>> GetAll();

        Task Update(T item);

        Task Remove(int id);

        void Dispose();
    }
}
