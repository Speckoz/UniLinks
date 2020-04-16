using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLink.Dependencies.Models;

namespace UniLink.API.Repository.Interfaces
{
	public interface ICourseRepository
	{
		Task<CourseModel> FindByCoordIdAndCourseId(Guid coordId, Guid CourseId);
	}
}
