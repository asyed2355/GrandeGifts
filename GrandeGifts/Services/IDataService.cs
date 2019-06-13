using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GrandeGifts.Services
{
    public interface IDataService<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Create(T Entity);
        void Delete(T Entity);
        void Update(T Entity);
        void UpdateMultiple(IEnumerable<T> Entities);
        T GetSingle(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Query(Expression<Func<T, bool>> predicate);
    }
}
