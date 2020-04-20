using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniLink.API.Data.VO;
using UniLink.Dependencies.Models;

namespace UniLink.API.Business.Interfaces
{
	public interface IStudentBusiness
	{
		Task<StudentVO> FindByIdTaskAsync(Guid id);

		Task<IList<StudentVO>> FindAllByCoordIdAndCourseId(Guid coordId, Guid courseId);

		Task<StudentVO> AddTaskAsync(StudentVO student);

		Task DeleteTaskAsync(Guid id);
	}
}