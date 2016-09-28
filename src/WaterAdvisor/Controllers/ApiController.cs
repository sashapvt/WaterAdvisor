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
using WaterLibrary;
using System.Globalization;

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

            var project = await _context.Project.Include(p => p.WaterIn).SingleOrDefaultAsync(m => m.Id == homeViewModel.P.Id);
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
            public string Value;
        }

        // Check if Project exists
        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }

        // Load project
        private void LoadProject(HomeViewModel model, Project project)
        {
            model.P.Id = project.Id;
            model.P.ProjectComment = project.ProjectComment;
            model.P.ProjectDate = project.ProjectDate;
            model.P.ProjectName = project.ProjectName;
            model.P.RecoveryRO = project.RecoveryRO;
            model.P.pHCorrected = project.pHCorrected;
            model.P.pHCorrection = project.pHCorrection;
            if (project.WaterIn != null) model.WaterIn.ImportWater(project.WaterIn);
        }

        // Save project
        private void SaveProject(HomeViewModel model, Project project)
        {
            project.ProjectComment = model.P.ProjectComment;
            project.ProjectDate = model.P.ProjectDate;
            project.ProjectName = model.P.ProjectName;
            project.RecoveryRO = model.P.RecoveryRO;
            project.pHCorrected = model.P.pHCorrected;
            project.pHCorrection = (ProjectBase.EnumpHCorrection) model.P.pHCorrection;
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
                    ((WaterComponent)waterIn[NameSplitted[1]])[NameSplitted[2]] = Convert.ToDouble(changedValueObject.Value, CultureInfo.InvariantCulture);
                }
                waterIn.ExportWater(project.WaterIn);
            }
            else
            {
                //Proceed other values
                switch (changedValueObject.Name)
                {
                    case "P.pHCorrection":
                        project.pHCorrection = (ProjectBase.EnumpHCorrection) Convert.ToInt32(changedValueObject.Value);
                        break;
                    case "P.pHCorrected":
                        project.pHCorrected = Convert.ToDouble(changedValueObject.Value, CultureInfo.InvariantCulture);
                        break;
                    case "P.RecoveryRO":
                        project.RecoveryRO = Convert.ToDouble(changedValueObject.Value, CultureInfo.InvariantCulture);
                        break;
                    case "PasteROSA":
                        // Parse ROSA HTML
                        double recovery;
                        var RosaResult = RosaParser.ParseRosa(changedValueObject.Value, project.WaterIn, out recovery);
                        if (RosaResult) project.RecoveryRO = recovery;
                        break;
                }
            }
        }

    }
}