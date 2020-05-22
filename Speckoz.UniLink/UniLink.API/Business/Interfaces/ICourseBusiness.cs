using System;
using System.Threading.Tasks;

using UniLink.Dependencies.Data.VO;

namespace UniLink.API.Business.Interfaces
{
	public interface ICourseBusiness
	{
		Task<CourseVO> AddTaskAsync(CourseVO course);

		Task<bool> ExistsCoordInCourseTaskAsync(Guid coordId, Guid courseId);

		Task<CourseVO> FindByCoordIdTaskAsync(Guid coordId);

		Task<CourseVO> FindByCourseIdTaskAsync(Guid courseId);

		Task<CourseVO> UpdateTaskAsync(CourseVO newCourse);

		Task DeleteAsync(CourseVO course);
	}
}