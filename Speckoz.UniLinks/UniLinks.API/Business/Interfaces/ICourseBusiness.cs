using System;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO;

namespace UniLinks.API.Business.Interfaces
{
	public interface ICourseBusiness
	{
		Task<CourseVO> AddTaskAsync(CourseVO course);

		Task<bool> ExistsWithNameTaskAsync(string courseName);

		Task<bool> ExistsCoordInCourseTaskAsync(Guid coordId, Guid courseId);

		Task<CourseVO> FindByCoordIdTaskAsync(Guid coordId);

		Task<CourseVO> FindByCourseIdTaskAsync(Guid courseId);

		Task<CourseVO> UpdateTaskAsync(CourseVO newCourse);

		Task DeleteAsync(Guid courseId);
	}
}