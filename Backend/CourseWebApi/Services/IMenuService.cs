using CourseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWebApi.Services
{
    public interface IMenuService
    {
        Task<IEnumerable<Menu>> GetMenusByRole(string roleName);
    }
}
