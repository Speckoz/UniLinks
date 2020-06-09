using System;

using UniLinks.Dependencies.Enums;

namespace UniLinks.Dependencies.Data.VO.Class
{
	public class ClassVO
	{
		public Guid ClassId { get; set; }

		public Guid CourseId { get; set; }

		public string URI { get; set; }

		public byte Period { get; set; }

		public WeekDaysEnum WeekDays { get; set; }
	}
}