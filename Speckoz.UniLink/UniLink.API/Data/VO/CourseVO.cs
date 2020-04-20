using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniLink.API.Data.VO
{
    public class CourseVO
    {
		public Guid CourseId { get; set; }
		public string Name { get; set; }
		public byte Periods { get; set; }
		public Guid CoordinatorId { get; set; }
	}
}
