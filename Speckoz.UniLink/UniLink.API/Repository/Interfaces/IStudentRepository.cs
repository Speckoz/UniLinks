using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLink.Dependencies.Models;

namespace UniLink.API.Repository.Interfaces
{
	public interface IStudentRepository
	{
		Task<StudentModel> AddTaskAsync(StudentModel student);

		Task<bool> ExistsByEmailTaskAsync(string email);

		Task<StudentModel> FindByIdTaskAsync(Guid id);

		Task<StudentModel> FindByEmailTaskAsync(string email);

		Task<List<StudentModel>> FindAllByCourseIdTaskAsync(Guid courseId);

		Task DeleteAsync(StudentModel student);

		Task<StudentModel> UpdateTaskAsync(StudentModel student, StudentModel newStudent);
	}
}