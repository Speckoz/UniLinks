using System;
using System.ComponentModel.DataAnnotations;

namespace UniLink.Dependencies.Models
{
	public class CourseModel
	{
		[Key]
		public Guid CourseId { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public byte Periods { get; set; }

		[Required]
		public Guid CoordinatorId { get; set; }
	}
}