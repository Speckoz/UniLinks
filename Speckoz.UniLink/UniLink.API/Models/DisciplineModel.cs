using System;
using System.ComponentModel.DataAnnotations;

namespace UniLink.Dependencies.Models
{
    public class DisciplineModel
    {
        [Key]
        public Guid DisciplineId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Teacher { get; set; }

        [Required]
        public byte Period { get; set; }

        [Required]
        public Guid CourseId { get; set; }

        [Required]
        public Guid ClassId { get; set; }
    }
}