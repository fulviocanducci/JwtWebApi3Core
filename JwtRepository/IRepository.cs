using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JwtRepository
{
   public interface IRepository<T>
   {
      T Add(T model);
      bool Update(T model);
      bool Remove(T model);
      bool Remove(Expression<Func<T, bool>> where);
      T Find(object id);
      T Find(Expression<Func<T, bool>> where);
      IEnumerable<T> Get();
      IEnumerable<T> Get<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> orderBy);
   }

   public interface IRepositoryAsync<T>
   {
      Task<T> AddAsync(T model);
      Task<bool> UpdateAsync(T model);
      Task<bool> RemoveAsync(T model);
      Task<bool> RemoveAsync(Expression<Func<T, bool>> where);
      Task<T> FindAsync(object id);
      Task<T> FindAsync(Expression<Func<T, bool>> where);
      Task<IEnumerable<T>> GetAsync();
      Task<IEnumerable<T>> GetAsync<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> orderBy);
   }

   
}
