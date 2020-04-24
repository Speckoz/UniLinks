using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Data.VO.Student;

namespace UniLink.API.Business.Interfaces
{
	public interface IStudentBusiness
	{
		Task<StudentVO> AuthUserTaskAsync(string email);

		Task<StudentVO> AddTaskAsync(StudentVO student);

		Task<StudentVO> FindByIdTaskAsync(Guid id);

		Task<IList<StudentDisciplineVO>> FindAllByCoordIdAndCourseId(Guid coordId, Guid courseId);

		Task DeleteTaskAsync(Guid id);
	}
}