using System;
using System.Threading.Tasks;

using UniLink.Dependencies.Models;

namespace UniLink.API.Repository.Interfaces
{
	public interface ICourseRepository
	{
		Task<CourseModel> AddTaskAsync(CourseModel course);

		Task<bool> ExistsCoordInCourseTaskAsync(Guid coordId, Guid courseId);

		Task<CourseModel> FindByCoordIdTaskAsync(Guid coordId);

		Task<CourseModel> FindByCourseIdTaskAsync(Guid courseId);

		Task<CourseModel> UpdateTaskAsync(CourseModel course, CourseModel newCourse);

		Task DeleteAsync(CourseModel course);
	}
}