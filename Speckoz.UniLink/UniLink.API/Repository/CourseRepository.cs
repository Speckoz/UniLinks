using Microsoft.EntityFrameworkCore;

using System;
using System.Threading.Tasks;

using UniLink.API.Data;
using UniLink.API.Repository.Interfaces;
using UniLink.Dependencies.Models;

namespace UniLink.API.Repository
{
	public class CourseRepository : BaseRepository, ICourseRepository
	{
		public CourseRepository(DataContext context) : base(context)
		{
		}

		public async Task<CourseModel> FindByCoordIdTaskAsync(Guid coordId) =>
			await _context.Courses.FirstOrDefaultAsync(x => x.CoordinatorId == coordId);

		public async Task<bool> ExistsCoordInCourseTaskAsync(Guid coordId, Guid courseId) => 
			await _context.Courses.AnyAsync(x => x.CoordinatorId == coordId && x.CourseId == courseId);
	}
}