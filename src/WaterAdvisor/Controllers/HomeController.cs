using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WaterAdvisor.Data;
using WaterAdvisor.Models.Project;

namespace WaterAdvisor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? Id)
        {
            // Todo: Fix cookies, maybe routing
            if (Id != null)
            {
                if (Request.Cookies["lastProjectOpenedId"] != null) Response.Cookies.Delete("lastProjectOpenedId");
                Response.Cookies.Append("lastProjectOpenedId", Id.ToString());
                return View(new HomeViewModel());
            }
            if (Request.Cookies["lastProjectOpenedId"] == null)
                return Redirect("/Project");
            else
                return Redirect("Home/Index/" + Request.Cookies["lastProjectOpenedId"]);
        }

        // GET: /Home/Error
        public ActionResult Error(int HttpCode, string message)
        {
            string Title;

            switch (HttpCode)
            {
                case 404:
                    // page not found
                    Title = "�������. ������� �� ��������";
                    break;
                case 500:
                    // server error
                    Title = "�������. �������� �� ������";
                    break;
                default:
                    Title = "�������� �������";
                    break;
            }

            ViewBag.Title = Title;
            ViewBag.HttpCode = HttpCode;
            ViewBag.Message = message;
            return View();
        }
    }
}