using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CourseRepositorys
{
    public interface IBaseRepository : IDisposable
    {
        Task<T> FindById<T>(Guid id) where T : class;

        IQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class;

        Task<bool> Any<T>(Expression<Func<T, bool>> funcWhere) where T : class;

        Task<T> Insert<T>(T t) where T : class;
        bool Update<T>(T t) where T : class;
        bool Delete<T>(T t) where T : class;

        int Commit();

        Task<int> CommitAsync();
    }
}
