using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WaterAdvisor.Data;
using WaterAdvisor.Models.Project;

namespace WaterAdvisor.Controllers
{
    [Produces("application/json")]
    //[Route("api/[controller]")]
    [Authorize]
    public class ApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApiController(ApplicationDbContext context)
        {
            _context = context;

        }

        // GET: Api/Get/1
        [HttpGet]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var project = await _context.Project.SingleOrDefaultAsync(m => m.Id == id);
            var project = _context.Project
                .Include(p => p.User)
                .Where(i => i.Id == id)
                .Single();

            if (project == null)
            {
                return NotFound();
            }

            var userId = User.Identity.Name;
            if (project.User.UserName != userId)
            {
                return Unauthorized();
            }

            return Ok(project);
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

            var userId = User.Identity.Name;
            if (project.User.Id != userId)
            {
                return Unauthorized();
            }

            return this.CreatedAtAction("Get", new { }, true);
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}