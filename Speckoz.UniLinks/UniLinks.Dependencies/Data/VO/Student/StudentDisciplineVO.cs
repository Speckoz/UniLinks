using System.Collections.Generic;

namespace UniLinks.Dependencies.Data.VO.Student
{
	public class StudentDisciplineVO
	{
		public StudentVO Student { get; set; }
		public List<DisciplineVO> Disciplines { get; set; }
	}
}