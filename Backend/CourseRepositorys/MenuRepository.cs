using CourseModels;
using CourseRepositorys.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CourseRepositorys
{
    public class MenuRepository :BaseRepository,IMenuRepository
    {
        public AppDbContext _context;

        public MenuRepository(AppDbContext context):base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Menu>> GetMenusByRole(string roleName)
        {
            return await base.Query<Menu>(m => m.Role.Contains(roleName)).ToListAsync();
        }
    }
}
