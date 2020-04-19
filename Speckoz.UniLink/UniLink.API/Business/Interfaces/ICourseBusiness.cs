using System;
using System.Threading.Tasks;

using UniLink.Dependencies.Models;

namespace UniLink.API.Business.Interfaces
{
	public interface ICourseBusiness
	{
		Task<CourseModel> FindByCoordIdTaskAsync(Guid coordId);
	}
}