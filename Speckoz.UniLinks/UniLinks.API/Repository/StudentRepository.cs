using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UniLinks.API.Data;
using UniLinks.API.Repository.Interfaces;
using UniLinks.Dependencies.Models;

namespace UniLinks.API.Repository
{
	public class StudentRepository : BaseRepository, IStudentRepository
	{
		public StudentRepository(DataContext context) : base(context)
		{
		}

		public async Task<StudentModel> AddTaskAsync(StudentModel student)
		{
			StudentModel addedStudent = (await _context.Students.AddAsync(student)).Entity;
			await _context.SaveChangesAsync();
			return addedStudent;
		}

		public async Task<bool> ExistsByEmailTaskAsync(string email) =>
			await _context.Students.AnyAsync(x => x.Email.ToLower().Equals(email.ToLower()));

		public async Task<bool> ExistsStudentWithDisciplineTaskAsync(Guid discipline) =>
			await _context.Students.AnyAsync(x => x.Disciplines.Contains(discipline.ToString()));

		public async Task<StudentModel> FindByStudentIdTaskAsync(Guid id) =>
			await _context.Students.Where(x => x.StudentId == id).SingleOrDefaultAsync();

		public async Task<StudentModel> FindByEmailTaskAsync(string email) =>
			await _context.Students.SingleOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));

		public async Task<List<StudentModel>> FindAllByCourseIdTaskAsync(Guid courseId) =>
			await _context.Students.Where(c => c.CourseId == courseId).ToListAsync();

		public async Task<StudentModel> UpdateTaskAsync(StudentModel student, StudentModel newStudent)
		{
			_context.Entry(student).CurrentValues.SetValues(newStudent);
			await _context.SaveChangesAsync();
			return newStudent;
		}

		public async Task DeleteAsync(StudentModel student)
		{
			_context.Students.Remove(student);
			await _context.SaveChangesAsync();
		}

		public async Task<int> FindCountByCourseIdTaskAsync(Guid courseId) =>
			await _context.Lessons.Where(x => x.CourseId == courseId).CountAsync();
	}
}