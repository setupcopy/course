using CourseModels;
using CourseRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWebApi.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        public LoginService(ILoginRepository logingRepository)
        {
            _loginRepository = logingRepository;
        }

        public async Task<User> GetUserByEmail(string email,string role)
        {
            return await _loginRepository.GetUserByEmail(email,role);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _loginRepository.GetUserByEmail(email);
        }

        public async Task<bool> SignInAsync(string email, string password)
        {
            return await _loginRepository.SignInAsync(email, password);
        }
    }
}
