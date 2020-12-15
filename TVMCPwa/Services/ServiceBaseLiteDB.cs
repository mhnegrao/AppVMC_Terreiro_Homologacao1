namespace AppVMC.Services
{
    //public class ServiceBaseLiteDB<T> : IServiceBase<T> where T : class
    //{
    //    /// <summary>
    //    /// Adiciona um registro no banco de dados LiteDB do projeto
    //    /// </summary>
    //    /// <param name="item"></param>
    //    /// <returns></returns>
    //    protected LiteCollection<T> _collection;

    //    public async Task Add(T item)
    //    {
    //        using var db = new LiteDatabase(Startup._database);
    //        var col = db.GetCollection<T>(typeof(T).Name);
    //        await Task.FromResult(col.Insert(item));
    //    }

    //    public void Dispose()
    //    {
    //    }

    //    public async Task<List<T>> GetAll()
    //    {
    //        using (var db = new LiteDatabase(Startup._database))
    //        {
    //            var col = db.GetCollection<T>(typeof(T).Name);

    //            return await Task.FromResult(col.FindAll().ToList());
    //        }
    //    }

    //    public async Task<T> GetById(int id)
    //    {
    //        using var db = new LiteDatabase(Startup._database);
    //        var col = db.GetCollection<T>(typeof(T).Name);
    //        var result = await Task.FromResult(col.FindById(id));
    //        return result;
    //    }

    //    public T GetNew()
    //    {
    //        T obj = default;
    //        obj = Activator.CreateInstance<T>();
    //        return obj;
    //    }

    //    public async Task Remove(int id)
    //    {
    //        using var db = new LiteDatabase(Startup._database);
    //        var col = db.GetCollection<T>(typeof(T).Name);
    //        await Task.FromResult(col.Delete(id));
    //    }

    //    public async Task Update(T item)
    //    {
    //        using var db = new LiteDatabase(Startup._database);
    //        var col = db.GetCollection<T>(typeof(T).Name);

    //        await Task.FromResult(col.Update(item));
    //    }

    //    public Task Update(T item, int id)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}