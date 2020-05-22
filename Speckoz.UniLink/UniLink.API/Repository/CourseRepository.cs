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

		public async Task<CourseModel> AddTaskAsync(CourseModel course)
		{
			CourseModel addedCourse = (await _context.Courses.AddAsync(course)).Entity;
			await _context.SaveChangesAsync();
			return addedCourse;
		}

		public async Task<CourseModel> FindByCoordIdTaskAsync(Guid coordId) =>
			await _context.Courses.FirstOrDefaultAsync(x => x.CoordinatorId == coordId);

		public async Task<bool> ExistsCoordInCourseTaskAsync(Guid coordId, Guid courseId) =>
			await _context.Courses.AnyAsync(x => x.CoordinatorId == coordId && x.CourseId == courseId);

		public async Task<CourseModel> FindByCourseIdTaskAsync(Guid courseId) =>
			await _context.Courses.SingleOrDefaultAsync(x => x.CourseId == courseId);

		public async Task<CourseModel> UpdateTaskAsync(CourseModel course, CourseModel newCourse)
		{
			_context.Entry(course).CurrentValues.SetValues(newCourse);
			await _context.SaveChangesAsync();
			return newCourse;
		}

		public async Task DeleteAsync(CourseModel course)
		{
			_context.Courses.Remove(course);
			await _context.SaveChangesAsync();
		}
	}
}