using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UniLink.API.Data;
using UniLink.API.Repository.Interfaces;
using UniLink.Dependencies.Models;

namespace UniLink.API.Repository
{
	public class StudentRepository : BaseRepository, IStudentRepository
	{
		public StudentRepository(DataContext context) : base(context)
		{
		}

		public async Task<StudentModel> AddTaskAsync(StudentModel student)
		{
			if ((await _context.Students.AddAsync(student)).Entity is StudentModel addedStudent)
			{
				await _context.SaveChangesAsync();
				return addedStudent;
			}
			return default;
		}

		public async Task<StudentModel> FindByIdTaskAsync(Guid id) => 
			await _context.Students.Where(x => x.StudentId == id).SingleOrDefaultAsync();

		public async Task<IList<StudentModel>> FindAllByCourseTaskAsync(Guid coordId, Guid courseId)
		{
			var course = await _context.Courses
				.Where(c => c.CourseId == courseId && c.CoordinatorId == coordId)
				.FirstOrDefaultAsync();

			return await _context.Students
				.Where(c => c.CourseId == course.CourseId)
				.ToListAsync();
		}

		public async Task DeleteTaskAsync(StudentModel student)
		{
			_context.Students.Remove(student);
			await _context.SaveChangesAsync();
		}
	}
}