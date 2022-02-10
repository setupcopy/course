using CourseModels;
using CourseRepositorys.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CourseUtility;

namespace CourseRepositorys
{
    public class UserRepository : BaseRepository,IUserRepository
    {
        public AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> AddUser(User user)
        {
            try
            {
                var newId = base.Query<User>(u => u.Id > 0).Select(u => u.Id).Max() + 1;
                user.Id = newId;
                await base.Insert<User>(user);
                var result = await base.CommitAsync();
                if (result <= 0)
                {
                    return null;
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            };
        }

        public async Task<bool> DeleteUser(User user)
        {
            try
            {
                base.Delete<User>(user);
                var result = await base.CommitAsync();
                if (result <= 0)
                {
                    return false;
                }

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PaginationList<User>> GetUsers(int pageSize, int pageNumber)
        {
            try
            {
                IQueryable<User> result  = base.Query<User>(u => u.Id > 0);
                return await PaginationList<User>.CreateAsync(pageNumber, pageSize, result);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                return await base.Query<User>(u => u.Id == id).FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> UpdateUser(User user)
        {
            try
            {
                base.Update<User>(user);
                var result = await base.CommitAsync();
                if (result <= 0)
                {
                    return null;
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
