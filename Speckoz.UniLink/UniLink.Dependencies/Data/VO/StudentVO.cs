using System;
using System.Collections.Generic;

namespace UniLink.Dependencies.Data.VO
{
	public class StudentVO
	{
		public Guid StudentId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public Guid CourseId { get; set; }
		public string Disciplines { get; set; }
		//REFATORAR ESSA LOGICA NO SITE
		public IList<DisciplineVO> DisciplinesList { get; set; }
		public string Token { get; set; }
	}
}