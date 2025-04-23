using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Authorize]           // ← Kräver inloggning
    [Route("projects")]
    public class ProjectsController : Controller
    {
        private readonly IProjectService _service;
        public ProjectsController(IProjectService service) => _service = service;

        // INDEX
        [HttpGet("")]
        public IActionResult Projects(string? filter)
        {
            var vm = new ProjectsIndexViewModel
            {
                AllCount = _service.CountAll(),
                StartedCount = _service.CountByStatus("Started"),
                CompletedCount = _service.CountByStatus("Completed"),
                CurrentFilter = (filter ?? "all").ToLower(),
                FilteredProjects = _service.GetFiltered(filter)
            };
            return View(vm);
        }

        // JSON för edit‐prefill
        [HttpGet("get/{id}")]
        public IActionResult GetProject(int id)
        {
            var p = _service.GetById(id);
            if (p == null) return NotFound();
            return Json(new
            {
                p.Id,
                p.ProjectName,
                p.ClientName,
                p.Description,
                StartDate = p.StartDate?.ToString("yyyy-MM-dd"),
                EndDate = p.EndDate?.ToString("yyyy-MM-dd"),
                p.Budget,
                p.Status
            });
        }

        // CREATE
        [HttpPost("add")]
        public IActionResult AddProject(Project model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid data submitted.");
                var all = _service.GetAll();
                var vm = new ProjectsIndexViewModel
                {
                    AllCount = all.Count,
                    StartedCount = _service.CountByStatus("Started"),
                    CompletedCount = _service.CountByStatus("Completed"),
                    CurrentFilter = "all",
                    FilteredProjects = all
                };
                return View("Projects", vm);
            }
            _service.Create(model);
            return RedirectToAction("Projects");
        }

        // UPDATE
        [HttpPost("edit/{id}")]
        public IActionResult EditProject(int id, Project model, string? filter)
        {
            if (id != model.Id || !ModelState.IsValid)
                return BadRequest();

            var existing = _service.GetById(id);
            if (existing == null) return NotFound();

            existing.ProjectName = model.ProjectName;
            existing.ClientName = model.ClientName;
            existing.Description = model.Description;
            existing.StartDate = model.StartDate;
            existing.EndDate = model.EndDate;
            existing.Budget = model.Budget;
            existing.Status = model.Status;

            _service.Update(existing);
            return RedirectToAction("Projects", new { filter = filter ?? "all" });
        }

        // DELETE
        [HttpPost("delete")]
        public IActionResult DeleteProject(int id, string? filter)
        {
            _service.Delete(id);
            return RedirectToAction("Projects", new { filter = filter ?? "all" });
        }
    }
}
