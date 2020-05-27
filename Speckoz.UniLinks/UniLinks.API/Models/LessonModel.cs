using System;
using System.ComponentModel.DataAnnotations;

namespace UniLinks.Dependencies.Models
{
	public class LessonModel
	{
		[Key]
		public Guid LessonId { get; set; }

		[Required]
		public string URI { get; set; }

		public string LessonSubject { get; set; }

		[Required]
		public Guid DisciplineId { get; set; }

		[Required]
		public Guid CourseId { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required]
		public int Duration { get; set; }

		[Required]
		public string RecordName { get; set; }
	}
}