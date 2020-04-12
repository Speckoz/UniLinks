using System;
using System.ComponentModel.DataAnnotations;

namespace UniLink.Dependencies.Models
{
	public class ClassModel
	{
		[Key]
		public Guid ClassId { get; set; }

		[Required]
		public string URI { get; set; }

		[Required]
		public string LessonSubject { get; set; }

		[Required]
		public DisciplineModel Discipline { get; set; }

		[Required]
		public DateTime Date { get; set; }
	}
}