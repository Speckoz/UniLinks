using System;
using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.API.Data.Converters;
using UniLink.API.Data.VO;
using UniLink.API.Repository.Interfaces;
using UniLink.Dependencies.Models;

namespace UniLink.API.Business
{
	public class CourseBusiness : ICourseBusiness
	{
		private readonly ICourseRepository _courseRepository;
		private readonly CourseConverter _converter;

		public CourseBusiness(ICourseRepository courseRepository)
		{
			_courseRepository = courseRepository;
			_converter = new CourseConverter();
		}

		public async Task<CourseVO> FindByCoordIdTaskAsync(Guid coordId) =>
			_converter.Parse(await _courseRepository.FindByCoordIdTaskAsync(coordId));
	}
}