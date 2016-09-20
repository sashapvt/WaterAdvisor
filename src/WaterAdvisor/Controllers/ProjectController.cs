using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WaterAdvisor.Data;
using WaterAdvisor.Models.Project;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WaterAdvisor.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WaterAdvisor.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public ProjectController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;    
            _userManager = userManager;
        }

        // GET: Project
        public async Task<IActionResult> Index()
        {
            var currentUserId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
            var projects = await _context.Project.Where(Project => Project.UserId == currentUserId).ToListAsync();
            return View(projects);
        }

        // GET: Project/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ProjectComment,ProjectDate,ProjectName")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.UserId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
                _context.Add(project);
                _context.SaveChanges();
                var lastId = _context.Project.Last().Id;
                if (Request.Cookies["lastProjectOpenedId"] != null) Response.Cookies.Delete("lastProjectOpenedId");
                Response.Cookies.Append("lastProjectOpenedId", lastId.ToString());
                return RedirectToAction("Index", "Home", lastId);
            }
            return View(project);
        }

        // GET: Project/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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

            return View(project);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectComment,ProjectDate,ProjectName")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            project.UserId = _context.Project.AsNoTracking().SingleOrDefault(m => m.Id == id).UserId;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home", id);
            }
            return View(project);
        }

        // GET: Project/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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

            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Project.SingleOrDefaultAsync(m => m.Id == id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}
