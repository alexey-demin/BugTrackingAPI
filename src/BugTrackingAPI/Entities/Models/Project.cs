using System;
using System.ComponentModel.DataAnnotations;

namespace BugTrackingAPI.Entities.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите наименование проекта")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите описание проекта")]
        public string Description { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
