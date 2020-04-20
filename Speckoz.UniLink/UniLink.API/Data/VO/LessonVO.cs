using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLink.Dependencies.Models;

namespace UniLink.API.Data.VO
{
    public class LessonVO
    {
		public Guid LessonId { get; set; }
		public string URI { get; set; }
		public string LessonSubject { get; set; }
		public DisciplineModel Discipline { get; set; }
		public DateTime Date { get; set; }
	}
}
