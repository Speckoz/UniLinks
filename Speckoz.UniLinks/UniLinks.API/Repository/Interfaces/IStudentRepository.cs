using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLinks.Dependencies.Models;

namespace UniLinks.API.Repository.Interfaces
{
	public interface IStudentRepository
	{
		Task<StudentModel> AddTaskAsync(StudentModel student);

		Task<bool> ExistsByEmailTaskAsync(string email);

		Task<int> FindCountByCourseIdTaskAsync(Guid courseId);

		Task<StudentModel> FindByStudentIdTaskAsync(Guid id);

		Task<StudentModel> FindByEmailTaskAsync(string email);

		Task<List<StudentModel>> FindAllByCourseIdTaskAsync(Guid courseId);

		Task<bool> ExistsStudentWithDisciplineTaskAsync(Guid disciplineId);

		Task DeleteAsync(StudentModel student);

		Task<StudentModel> UpdateTaskAsync(StudentModel student, StudentModel newStudent);
	}
}