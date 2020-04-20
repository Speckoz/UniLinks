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

		public async Task<StudentModel> FindByIdTaskAsync(int id) => 
			await _context.Students.Include(x => x.Course).Where(x => x.Id == id).SingleOrDefaultAsync();

		public async Task<IList<StudentModel>> FindAllByCourseTaskAsync(Guid coordId, Guid courseId)
		{
			List<StudentModel> students = await _context.Students
				.Include(x => x.Course)
				.Where(x => x.Course.CoordinatorId == coordId && x.Course.CourseId == courseId)
				.ToListAsync();

			foreach (StudentModel student in students)
				student.User = await _context.Users.Where(u => u.UserId == student.StudentId)
					.Select(x => x.ToUserBaseModel()).SingleOrDefaultAsync();

			return students;
		}

		public async Task DeleteTaskAsync(StudentModel student)
		{
			_context.Students.Remove(student);
			await _context.SaveChangesAsync();
		}
	}
}