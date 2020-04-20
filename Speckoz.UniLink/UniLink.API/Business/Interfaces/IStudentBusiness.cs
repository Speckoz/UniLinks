using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLink.Dependencies.Data.VO;

namespace UniLink.API.Business.Interfaces
{
	public interface IStudentBusiness
	{
		Task<StudentVO> AuthUserTaskAsync(string email);

		Task<StudentVO> AddTaskAsync(StudentVO student);

		Task<StudentVO> FindByIdTaskAsync(Guid id);

		Task<IList<StudentVO>> FindAllByCoordIdAndCourseId(Guid coordId, Guid courseId);

		Task DeleteTaskAsync(Guid id);
	}
}