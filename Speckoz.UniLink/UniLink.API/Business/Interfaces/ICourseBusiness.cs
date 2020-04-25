using System;
using System.Threading.Tasks;

using UniLink.Dependencies.Data.VO;

namespace UniLink.API.Business.Interfaces
{
	public interface ICourseBusiness
	{
		Task<bool> ExistsCoordInCourseTaskAsync(Guid coordId, Guid courseId);

		Task<CourseVO> FindByCoordIdTaskAsync(Guid coordId);
	}
}