using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLink.Dependencies.Models;

namespace UniLink.API.Data.VO
{
    public class StudentVO
    {
		public int Id { get; set; }
		public Guid UserId { get; set; }
		public CourseModel Course { get; set; }
		public UserModel User { get; set; }
	}
}
