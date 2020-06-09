using System;

namespace UniLinks.Dependencies.Data.VO.Student
{
	public class AuthStudentVO
	{
		public Guid StudentId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public Guid CourseId { get; set; }
		public string Disciplines { get; set; }
		public string Token { get; set; }
	}
}