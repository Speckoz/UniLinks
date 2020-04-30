using System;

namespace UniLink.Dependencies.Data.VO
{
	public class DisciplineVO
	{
		public Guid DisciplineId { get; set; }
		public string Name { get; set; }
		public string Teacher { get; set; }
		public byte Period { get; set; }
		public Guid CourseId { get; set; }
		public Guid ClassId { get; set; }
	}
}