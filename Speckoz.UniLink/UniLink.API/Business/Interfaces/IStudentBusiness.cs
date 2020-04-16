using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLink.Dependencies.Models;

namespace UniLink.API.Business.Interfaces
{
	public interface IStudentBusiness
	{
		Task<IList<StudentModel>> FindAllByCourse(Guid coordId, Guid courseId);

		Task<StudentModel> AddTaskAsync(StudentModel student);

		Task DeleteTaskAsync(int id);
	}
}