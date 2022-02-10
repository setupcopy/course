using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;
using CourseRepositorys.DataBase;
using CourseModels;

namespace CourseRepositorys
{
    public class LoginRepository : BaseRepository,ILoginRepository
    {
        public AppDbContext _context;

        public LoginRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }


        /// <summary>
        /// Sign In
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public async Task<bool> SignInAsync(string email, string password)
        {
            return await base.Any<User>(u => u.Email.Equals(email) && u.Password.Equals(password));
        }

        public async Task<User> GetUserByEmail(string email,string role)
        {
            return await base.Query<User>(u => u.Email.Contains(email) && u.Role.Contains(role)).FirstOrDefaultAsync();
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await base.Query<User>(u => u.Email.Contains(email)).FirstOrDefaultAsync();
        }
    }
}
