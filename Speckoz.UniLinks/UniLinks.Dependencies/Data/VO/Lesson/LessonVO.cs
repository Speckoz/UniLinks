using System;
using System.Text.Json.Serialization;

namespace UniLinks.Dependencies.Data.VO
{
	public class LessonVO
	{
		public Guid LessonId { get; set; }
		public string URI { get; set; }
		public string LessonSubject { get; set; }
		public Guid DisciplineId { get; set; }
		public Guid CourseId { get; set; }

		[JsonPropertyName("created")]
		public DateTime Date { get; set; }

		public int Duration { get; set; }

		[JsonPropertyName("name")]
		public string RecordName { get; set; }
	}
}