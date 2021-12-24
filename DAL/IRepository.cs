using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public interface IRepository<TEntity>
    {
        List<TEntity> FetchAll();
        IQueryable<TEntity> Query { get; }
        TEntity Find(string id);

        TEntity Update(TEntity entity);
        TEntity Add(TEntity entity);
        TEntity Delete(TEntity entity);
        void Save();
    }
}