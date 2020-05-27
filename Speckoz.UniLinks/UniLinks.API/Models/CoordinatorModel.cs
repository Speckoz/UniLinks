using System;
using System.ComponentModel.DataAnnotations;

namespace UniLinks.API.Models
{
	public class CoordinatorModel
	{
		[Key]
		public Guid CoordinatorId { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		public Guid CourseId { get; set; }
	}
}