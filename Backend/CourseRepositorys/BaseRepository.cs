using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CourseRepositorys.DataBase;

namespace CourseRepositorys
{
    public class BaseRepository : IBaseRepository
    {
        protected AppDbContext Context { get;private set; }

        public BaseRepository(AppDbContext context)
        {
            Context = context;
        }

        public IQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class
        {
            return Context.Set<T>().Where<T>(funcWhere);
        }

        public async Task<T> FindById<T>(Guid id) where T : class
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<bool> Any<T>(Expression<Func<T, bool>> funcWhere) where T : class
        {
            return await Context.Set<T>().AnyAsync<T>(funcWhere);
        }

        public async Task<T> Insert<T>(T t) where T : class
        {
            await Context.Set<T>().AddAsync(t);
            return t;
        }

        public bool Update<T>(T t) where T : class
        {
            if (t == null)
            {
                return false;
            }

            Context.Set<T>().Attach(t);
            Context.Entry<T>(t).State = EntityState.Modified;
            return true;
        }

        public bool Delete<T>(T t) where T : class
        {
            if (t == null)
            {
                return false;
            }

            Context.Set<T>().Remove(t);
            return true;
        }

        public int Commit()
        {
            return Context.SaveChanges();
        }


        public virtual void Dispose()
        {
            Context.Dispose();
        }

        public async Task<int> CommitAsync()
        {
            return await Context.SaveChangesAsync(); ;
        }
    }
}
