using System;
using System.ComponentModel.DataAnnotations;

namespace UniLink.Dependencies.Models
{
    public class StudentModel
    {
        [Key]
        public Guid StudentId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public Guid CourseId { get; set; }

        [Required]
        public string Disciplines { get; set; }
    }
}