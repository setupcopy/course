using CourseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRepositorys.DataBase
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> user_entity { get; set; }
        public DbSet<Menu> Menus { get; set; }

        //输出到debug输出
        public static readonly LoggerFactory LoggerFactory =
        new LoggerFactory(new[] { new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()});
        // 输出到Console
        //public static readonly LoggerFactory LoggerFactory =
        //       new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(LoggerFactory);
        }

    }
}
