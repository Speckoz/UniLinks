using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLink.Dependencies.Models;

namespace UniLink.API.Repository.Interfaces
{
	public interface IStudentRepository
	{
		Task<StudentModel> AddTaskAsync(StudentModel student);

		Task<StudentModel> FindByIdTaskAsync(Guid id);

		Task<StudentModel> FindByEmailTaskAsync(string email);

		Task<IList<StudentModel>> FindAllByCourseTaskAsync(Guid coordId, Guid courseId);

		Task DeleteTaskAsync(StudentModel student);
	}
}