using System;
using System.ComponentModel.DataAnnotations;

namespace UniLinks.API.Models
{
    public class ClassModel
    {
        [Key]
        public Guid ClassId { get; set; }

        [Required]
        public Guid CourseId { get; set; }

        [Required]
        public string URI { get; set; }

        [Required]
        public byte Period { get; set; }
    }
}