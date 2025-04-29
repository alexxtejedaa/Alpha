using System;
using System.ComponentModel.DataAnnotations;

// Modellerna har jag delvis använt AI, dock är de väldigt enkla och förklarar sig själva //

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

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public decimal? Budget { get; set; } = 0;

        public string Status { get; set; } = "Started";
    }
}
