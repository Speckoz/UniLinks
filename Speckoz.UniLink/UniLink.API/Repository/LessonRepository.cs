using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

using UniLink.API.Data;
using UniLink.API.Repository.Interfaces;
using UniLink.Dependencies.Enums;
using UniLink.Dependencies.Models;

namespace UniLink.API.Repository
{
	public class LessonRepository : BaseRepository, ILessonRepository
	{
		public LessonRepository(DataContext context) : base(context)
		{
		}

		public async Task<LessonModel> AddTaskAsync(LessonModel lesson)
		{
			if ((await _context.AddAsync(lesson)).Entity is LessonModel addedLesson)
			{
				await _context.SaveChangesAsync();
				return addedLesson;
			}
			return default;
		}

		public async Task<LessonModel> FindByCourseTaskAsync(Guid courseId, byte period) =>
			await _context.Lessons
			.Include(c => c.Discipline)
			.Where(c => c.Discipline.Course.CourseId == courseId && c.Discipline.Period == period).SingleOrDefaultAsync();

		public Task<LessonModel> FindByDateTaskAsync(DateTime dateTime, LessonShiftEnum lessonShift) =>
			throw new NotImplementedException();

		public async Task<LessonModel> FindByIdTaskAsync(Guid lessonId) =>
			await _context.Lessons
			.Include(c => c.Discipline)
			.Where(c => c.LessonId == lessonId).SingleOrDefaultAsync();

		public async Task<LessonModel> FindByURITaskAsync(string uri) =>
			await _context.Lessons.Include(c => c.Discipline).Where(c => c.URI == uri).SingleOrDefaultAsync();

		public async Task<LessonModel> UpdateTaskAsync(LessonModel lesson, LessonModel newLesson)
		{
			_context.Entry(lesson).CurrentValues.SetValues(newLesson);
			await _context.SaveChangesAsync();
			return newLesson;
		}

		public async Task DeleteTaskAsync(LessonModel lesson)
		{
			_context.Lessons.Remove(lesson);
			await _context.SaveChangesAsync();
		}
	}
}