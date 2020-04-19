using System;
using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.API.Repository.Interfaces;
using UniLink.Dependencies.Models;

namespace UniLink.API.Business
{
	public class CourseBusiness : ICourseBusiness
	{
		private readonly ICourseRepository _courseRepository;

		public CourseBusiness(ICourseRepository courseRepository)
		{
			_courseRepository = courseRepository;
		}

		public async Task<CourseModel> FindByCoordIdTaskAsync(Guid coordId) =>
			await _courseRepository.FindByCoordIdTaskAsync(coordId);
	}
}