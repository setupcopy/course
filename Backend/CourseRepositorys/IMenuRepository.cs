using CourseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRepositorys
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetMenusByRole(string roleName);
    }
}
