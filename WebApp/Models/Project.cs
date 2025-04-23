using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Project Name is required.")]
        public string ProjectName { get; set; } = null!;

        [Required(ErrorMessage = "Client Name is required.")]
        public string ClientName { get; set; } = null!;

        public string Description { get; set; } = "";

        // Datumfält för Figma-design
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Budget, med decimal(18,2) => se OnModelCreating i ApplicationDbContext
        public decimal? Budget { get; set; } = 0;

        // Status: "Started" eller "Completed" (eller "All" i filter). 
        // Du vill kanske defaulta till "Started" om du inte vill ha "Planned".
        public string Status { get; set; } = "Started";
    }
}
