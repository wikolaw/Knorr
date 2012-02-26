using System.Collections.Generic;

namespace Knorr.Core.FileDb
{
    public interface IRepository<T> where T : class, new()
    {
        void Create(T model);
        IEnumerable<T> Read();
        void Update(T model);
        void Delete(T model);
    }
}