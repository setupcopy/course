﻿using CourseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseRepositorys
{
    public interface ILoginRepository
    {
        Task<bool> SignInAsync(string email,string password);

        Task<User> GetUserByEmail(string email,string role);
        Task<User> GetUserByEmail(string email);
    }
}
