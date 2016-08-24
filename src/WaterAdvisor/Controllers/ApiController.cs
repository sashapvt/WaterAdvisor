using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WaterAdvisor.Data;
using WaterAdvisor.Models.Project;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using WaterAdvisor.Models;

namespace WaterAdvisor.Controllers
{
    [Produces("application/json")]
    //[Route("api/[controller]")]
    [Authorize]
    public class ApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public ApiController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Api/Get/1
        [HttpGet]
        public async Task<IActionResult> Get(int? id)
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

            return Json(project, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });
        }

        // POST: Api/Post
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var currentUserId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
                if (project.UserId != currentUserId)
                {
                    return Unauthorized();
                }
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

            return this.CreatedAtAction("Get", new { }, true);
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}