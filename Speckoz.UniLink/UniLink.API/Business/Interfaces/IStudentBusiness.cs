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

		Task<IList<StudentDisciplineVO>> FindAllByCoordIdAndCourseId(Guid coordId, Guid courseId);

		Task DeleteTaskAsync(Guid id);

		Task<StudentVO> UpdateTaskAsync(StudentVO student);
	}
}