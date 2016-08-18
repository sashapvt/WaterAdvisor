using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WaterAdvisor.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // GET: Project/Edit/3
        public IActionResult Edit(int? id)
        {
             return View("Index");
        }

    }
}