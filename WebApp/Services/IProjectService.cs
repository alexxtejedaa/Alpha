using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Services

// Definerar ett abstraherat kontrakt för CRUD-operationer, filtrering och räkning av projekt-entiteter. 

{
    public interface IProjectService
    {
        List<Project> GetAll();
        List<Project> GetFiltered(string? filter);
        Project? GetById(int id);
        void Create(Project project);
        void Update(Project project);
        void Delete(int id);
        int CountAll();
        int CountByStatus(string status);
    }
}
