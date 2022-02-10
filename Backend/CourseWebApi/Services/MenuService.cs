using CourseModels;
using CourseRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWebApi.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public async Task<IEnumerable<Menu>> GetMenusByRole(string roleName)
        {
            return await _menuRepository.GetMenusByRole(roleName);
        }
    }
}
