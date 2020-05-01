using System;
using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.API.Data.Converters;
using UniLink.API.Repository.Interfaces;
using UniLink.Dependencies.Data.VO;

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

        public async Task<bool> ExistsCoordInCourseTaskAsync(Guid coordId, Guid courseId) =>
            await _courseRepository.ExistsCoordInCourseTaskAsync(coordId, courseId);
    }
}