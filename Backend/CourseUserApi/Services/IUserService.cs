using CourseModels;
using CourseUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseUserApi.Services
{
    public interface IUserService
    {
        Task<User> AddUser(User user);
        Task<bool> DeleteUser(User user);
        Task<User> UpdateUser(User user);
        Task<PaginationList<User>> GetUsers(int pageSize, int pageNumber);
        Task<User> GetUserById(int id);

        void SetHashedPassword(User user);

        bool ValidateDataOfUserRequest(string email,string password,string passwordConfirmed);
    }
}
