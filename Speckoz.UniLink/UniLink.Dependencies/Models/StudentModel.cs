using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniLink.Dependencies.Models
{
	public class StudentModel
	{
		[Key]
		public Guid StudentId { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public Guid CourseId { get; set; }
	}
}