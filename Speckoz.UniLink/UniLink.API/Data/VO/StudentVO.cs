using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLink.Dependencies.Models;

namespace UniLink.API.Data.VO
{
    public class StudentVO
    {
		public Guid StudentId { get; set; }
		public string Email { get; set; }
		public Guid CourseId { get; set; }
	}
}
