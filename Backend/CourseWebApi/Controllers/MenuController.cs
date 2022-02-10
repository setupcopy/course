using CourseWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWebApi.Utilitys;
using System.Threading;
using CourseWebApi.Models;

namespace CourseWebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class MenuController:ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenusByRole([FromQuery]string roleName)
        {
            var menus = await _menuService.GetMenusByRole(roleName);

            if (menus == null)
            {
                return NoContent();
            }
            return Ok(menus);
        }

        [HttpGet("sse")]
        public async Task SSE()
        {
            await HttpContext.SSEInitAsync();
            Thread.Sleep(1500);
            await HttpContext.SSESendEventAsync(
                    new SSEEvent("eventTest",new {courseName = "C#",localcation = "sydney"})
                    {
                       Id = "001",
                       Retry = 10
                    }
                );
        }
    }
}
