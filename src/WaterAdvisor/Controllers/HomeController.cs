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

        public IActionResult Index()
        {
            return View(new HomeViewModel());
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