using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLink.Dependencies.Data.VO.Student;

namespace UniLink.API.Business.Interfaces
{
	public interface IStudentBusiness
	{
		Task<StudentVO> AuthUserTaskAsync(string email);

		Task<StudentDisciplineVO> AddTaskAsync(StudentVO student);

		Task<bool> ExistsByEmailTaskAsync(string email);

		Task<StudentVO> FindByIdTaskAsync(Guid id);

		Task<List<StudentDisciplineVO>> FindAllByCoordIdAndCourseId(Guid coordId, Guid courseId);

		Task<StudentVO> UpdateTaskAsync(StudentVO student, StudentVO newStudent);

		Task DeleteTaskAsync(Guid id);
	}
}