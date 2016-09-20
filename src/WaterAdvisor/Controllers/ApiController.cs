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

            var homeViewModel = new HomeViewModel();
            var project = await _context.Project.Include(p => p.WaterIn).SingleOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            var currentUserId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
            if (project.UserId != currentUserId)
            {
                return Unauthorized();
            }

            LoadProject(homeViewModel, project);

            return Json(homeViewModel, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });
        }

        // POST: Api/Post
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HomeViewModel homeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = await _context.Project.Include(p => p.WaterIn).SingleOrDefaultAsync(m => m.Id == homeViewModel.Id);
            if (project == null)
            {
                return NotFound();
            }

            var currentUserId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
            if (project.UserId != currentUserId)
            {
                return Unauthorized();
            }

            try
            {
                SaveProject(homeViewModel, project);
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

        // POST: Api/PostPartial
        [HttpPost]
        public async Task<IActionResult> PostPartial([FromBody] ChangedValueObject changedValueObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = await _context.Project.Include(p => p.WaterIn).SingleOrDefaultAsync(m => m.Id == changedValueObject.ProjectId);
            if (project == null)
            {
                return NotFound();
            }

            var currentUserId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;
            if (project.UserId != currentUserId)
            {
                return Unauthorized();
            }

            try
            {
                SaveProjectPartial(changedValueObject, project);
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

        // Helpers section

        // Class ChangedValueObject
        public class ChangedValueObject
        {
            public int ProjectId;
            public string Name;
            public double Value;
        }

        // Check if Project exists
        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }

        // Load project
        private void LoadProject(HomeViewModel model, Project project)
        {
            model.Id = project.Id;
            model.ProjectComment = project.ProjectComment;
            model.ProjectDate = project.ProjectDate;
            model.ProjectName = project.ProjectName;
            model.RecoveryRO = project.RecoveryRO;
            model.pHCorrected = project.pHCorrected;
            model.pHCorrection = project.pHCorrection;
            model.pHCorrectionAcidDose = project.pHCorrectionAcidDose;
            if (project.WaterIn != null) model.WaterIn.ImportWater(project.WaterIn);
        }

        // Save project
        private void SaveProject(HomeViewModel model, Project project)
        {
            project.ProjectComment = model.ProjectComment;
            project.ProjectDate = model.ProjectDate;
            project.ProjectName = model.ProjectName;
            project.RecoveryRO = model.RecoveryRO;
            project.pHCorrected = model.pHCorrected;
            project.pHCorrection = (ProjectBase.EnumpHCorrection) model.pHCorrection;
            project.pHCorrectionAcidDose = model.pHCorrectionAcidDose;
            if (project.WaterIn == null) project.WaterIn = new Water();
            model.WaterIn.ExportWater(project.WaterIn);
        }

        // Save project partial
        private void SaveProjectPartial(ChangedValueObject changedValueObject, Project project)
        {
            if (changedValueObject.Name.Contains("WaterIn."))
            {
                // Proceed WaterIn object
                if (project.WaterIn == null) project.WaterIn = new Water();
                var waterIn = new WaterList();
                waterIn.ImportWater(project.WaterIn);

                // Parse value like this: WaterIn.Na.Value
                string[] NameSplitted = changedValueObject.Name.Split("."[0]);
                if (NameSplitted.Length == 3 && NameSplitted[0] == "WaterIn")
                {
                    ((WaterComponent)waterIn[NameSplitted[1]])[NameSplitted[2]] = changedValueObject.Value;
                }
                waterIn.ExportWater(project.WaterIn);
            }
            else
            {
                //Proceed other values
                switch (changedValueObject.Name)
                {
                    case "pHCorrection":
                        project.pHCorrection = (ProjectBase.EnumpHCorrection) Convert.ToInt32(changedValueObject.Value);
                        break;
                    case "pHCorrected":
                        project.pHCorrected = changedValueObject.Value;
                        break;
                    case "pHCorrectionAcidDose":
                        project.pHCorrectionAcidDose = changedValueObject.Value;
                        break;
                    case "RecoveryRO":
                        project.RecoveryRO = changedValueObject.Value;
                        break;
                }
            }
        }

    }
}