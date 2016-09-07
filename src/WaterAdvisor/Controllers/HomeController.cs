using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaterAdvisor.Data;
using WaterAdvisor.Models;
using WaterAdvisor.Models.Project;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WaterAdvisor.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                if (Request.Cookies["lastProjectOpenedId"] == null)
                    return Redirect("/Project");
                else
                    return Redirect("/?id=" + Request.Cookies["lastProjectOpenedId"]);
            }
            else
            {
                if (Request.Cookies["lastProjectOpenedId"] != null) Response.Cookies.Delete("lastProjectOpenedId");
                Response.Cookies.Append("lastProjectOpenedId",id.ToString());

                var project = await _context.Project.SingleOrDefaultAsync(m => m.Id == id);

                if (project == null)
                {
                    return NotFound();
                }

                var currentUserId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
                if (project.UserId != currentUserId)
                {
                    return Unauthorized();
                }
                var homeViewModel = new HomeViewModel();
                homeViewModel.WaterIn = new WaterList();
                homeViewModel.Id = project.Id;
                homeViewModel.ProjectName = project.ProjectName;

                return View(homeViewModel);
            }
        }

        // GET: /Home/Error
        public ActionResult Error(int HttpCode, string message)
        {
            string Title;

            switch (HttpCode)
            {
                case 404:
                    // page not found
                    Title = "Помилка. Сторінку не знайдено";
                    break;
                case 500:
                    // server error
                    Title = "Помилка. Проблема на сервері";
                    break;
                default:
                    Title = "Загальна помилка";
                    break;
            }

            ViewBag.Title = Title;
            ViewBag.HttpCode = HttpCode;
            ViewBag.Message = message;
            return View();
        }

    }
}