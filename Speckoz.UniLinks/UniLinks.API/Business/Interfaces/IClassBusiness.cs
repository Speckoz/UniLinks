using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO;

namespace UniLinks.API.Business.Interfaces
{
	public interface IClassBusiness
	{
		Task<ClassVO> AddTasAsync(ClassVO @class);

		Task<ClassVO> FindByClassIdTaskAsync(Guid classId);

		Task<List<ClassVO>> FindByRangeClassIdTaskAsync(List<Guid> classIds);

		Task<ClassVO> FindByURITaskAsync(string uri);

		Task<List<ClassVO>> FindAllByCourseIdAndPeriodTaskAsync(Guid courseId, int period);

		Task<List<ClassVO>> FindAllByCourseIdTaskAsync(Guid courseId);

		Task<ClassVO> UpdateTaskAsync(ClassVO newClass);

		Task RemoveAsync(Guid classId);
	}
}