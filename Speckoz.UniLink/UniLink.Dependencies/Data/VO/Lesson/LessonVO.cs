using System;

namespace UniLink.Dependencies.Data.VO
{
	public class LessonVO
	{
		public Guid LessonId { get; set; }
		public string URI { get; set; }
		public string LessonSubject { get; set; }
		public Guid DisciplineId { get; set; }
		public DateTime Date { get; set; }
	}
}