using System;
using System.ComponentModel.DataAnnotations;

namespace BugTrackingAPI.Entities.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите наименование задачи")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите описание задачи")]
        public string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Приоритет должен быть больше 0")]
        [Required(ErrorMessage = "Укажите приоритет")]
        public int Priority { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public int StatusId { get; set; }
        public TaskStatus Status { get; set; }

        [Required(ErrorMessage = "Укажите проект")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
