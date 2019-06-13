using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
// Added namespaces:
using GrandeGifts.Data_Access;
using Microsoft.EntityFrameworkCore;

namespace GrandeGifts.Services
{
    public class DataService<T> : IDataService<T> where T : class
    {
        private ApplicationDbContext _context;
        private DbSet<T> _dbSet;

        public DataService()
        {
            _context = new ApplicationDbContext();
            _dbSet = _context.Set<T>();
        }

        public void Create(T Entity)
        {
            _dbSet.Add(Entity);
            _context.SaveChanges();
        }

        public void Delete(T Entity)
        {
            _dbSet.Remove(Entity);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public void Update(T Entity)
        {
            _dbSet.Update(Entity);
            _context.SaveChanges();
        }

        public void UpdateMultiple(IEnumerable<T> Entities)
        {
            foreach(var E in Entities)
            {
                _dbSet.Update(E);
            } 
            _context.SaveChanges();
        }
    }
}
