using System.Collections.Generic;
using System.Linq;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _db;
        public ProjectService(ApplicationDbContext db) => _db = db;

        public List<Project> GetAll() => _db.Projects.ToList();

        public List<Project> GetFiltered(string? filter)
        {
            var q = _db.Projects.AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                switch (filter.ToLower())
                {
                    case "started":
                        q = q.Where(p => p.Status == "Started");
                        break;
                    case "completed":
                        q = q.Where(p => p.Status == "Completed");
                        break;
                }
            }
            return q.ToList();
        }

        public Project? GetById(int id) => _db.Projects.Find(id);

        public void Create(Project project)
        {
            _db.Projects.Add(project);
            _db.SaveChanges();
        }

        public void Update(Project project)
        {
            _db.Projects.Update(project);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var p = _db.Projects.Find(id);
            if (p != null)
            {
                _db.Projects.Remove(p);
                _db.SaveChanges();
            }
        }

        public int CountAll() => _db.Projects.Count();

        public int CountByStatus(string status) => _db.Projects.Count(p => p.Status == status);
    }
}
