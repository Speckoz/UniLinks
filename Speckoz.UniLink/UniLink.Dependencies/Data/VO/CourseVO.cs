using System;

namespace UniLink.Dependencies.Data.VO
{
	public class CourseVO
	{
		public Guid CourseId { get; set; }
		public string Name { get; set; }
		public byte Periods { get; set; }
		public Guid CoordinatorId { get; set; }
	}
}