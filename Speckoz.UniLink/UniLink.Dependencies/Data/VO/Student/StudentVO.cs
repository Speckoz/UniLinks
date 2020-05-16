using System;
using System.Collections.Generic;

namespace UniLink.Dependencies.Data.VO.Student
{
	public class StudentVO
	{
		public Guid StudentId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public Guid CourseId { get; set; }
		public List<DisciplineVO> Disciplines { get; set; }
		public string Token { get; set; }
	}
}