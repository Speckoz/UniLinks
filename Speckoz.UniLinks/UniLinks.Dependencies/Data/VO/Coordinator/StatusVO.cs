using System.Collections.Generic;

using UniLinks.Dependencies.Data.VO.Lesson;

namespace UniLinks.Dependencies.Data.VO.Coordinator
{
	public class StatusVO
	{
		public int StudentsCount { get; set; }
		public int DisciplinesCount { get; set; }
		public int ClassesCouunt { get; set; }
		public int LessonsCount { get; set; }
		public List<LessonDisciplineVO> FiveLastLessons { get; set; }
	}
}