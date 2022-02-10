using CourseModels;
using CourseUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRepositorys
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<bool> DeleteUser(User user);
        Task<User> UpdateUser(User user);
        Task<PaginationList<User>> GetUsers(int pageSize, int pageNumber);
        Task<User> GetUserById(int id);
    }
}
