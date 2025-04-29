using System.Collections.Generic;

// Modellerna har jag delvis använt AI, dock är de väldigt enkla och förklarar sig själva //

namespace WebApp.Models
{
    public class ProjectsIndexViewModel
    {
        public List<Project> FilteredProjects { get; set; } = new();
        public int AllCount { get; set; }
        public int StartedCount { get; set; }
        public int CompletedCount { get; set; }
        public string CurrentFilter { get; set; } = "all";
    }
}
