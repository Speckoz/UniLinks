using System;
using System.ComponentModel.DataAnnotations;

namespace UniLink.Dependencies.Models
{
	public class LessonModel
	{
		[Key]
		public Guid LessonId { get; set; }

		[Required]
		public string URI { get; set; }

		[Required]
		public string LessonSubject { get; set; }

		[Required]
		public Guid DisciplineId { get; set; }

		[Required]
		public DateTime Date { get; set; }
	}
}