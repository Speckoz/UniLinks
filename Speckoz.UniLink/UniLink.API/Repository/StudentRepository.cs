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

		public async Task<IList<StudentModel>> FindAllByCourseTaskAsync(Guid coordId, Guid courseId)
		{
			List<StudentModel> students = await _context.Students
				.Include(x => x.Course)
				.Where(x => x.Course.CoordinatorId == coordId && x.Course.CourseId == courseId)
				.ToListAsync();

			students.ForEach(x =>
			{
				x.User = _context.Users.Where(u => u.UserId == x.UserId).Select(x => new UserBaseModel
				{
					UserId = x.UserId,
					Name = x.Name,
					Email = x.Email,
					UserType = x.UserType
				}).SingleOrDefault();
			});
			return students;
		}

		public async Task DeleteTaskAsync(StudentModel student)
		{
			_context.Students.Remove(student);
			await _context.SaveChangesAsync();
		}
	}
}