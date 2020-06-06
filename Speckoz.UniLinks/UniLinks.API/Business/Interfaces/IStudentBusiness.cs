using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO.Student;

namespace UniLinks.API.Business.Interfaces
{
	public interface IStudentBusiness
	{
		Task<AuthStudentVO> AuthUserTaskAsync(string email);

		Task<StudentVO> AddTaskAsync(StudentVO student);

		Task<bool> ExistsByEmailTaskAsync(string email);

		Task<StudentVO> FindByStudentIdTaskAsync(Guid id);

		Task<List<StudentVO>> FindAllByCourseIdTaskAsync(Guid courseId);

		Task<StudentVO> UpdateTaskAsync(StudentVO student, StudentVO newStudent);

		Task DeleteTaskAsync(Guid id);
	}
}