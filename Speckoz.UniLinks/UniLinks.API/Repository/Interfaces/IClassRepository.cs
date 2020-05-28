using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UniLinks.API.Models;

namespace UniLinks.API.Repository.Interfaces
{
	public interface IClassRepository
	{
		Task<ClassModel> AddTasAsync(ClassModel @class);

		Task<ClassModel> FindByClassIdTaskAsync(Guid classId);

		Task<ClassModel> FindByURITaskAsync(string uri);

		Task<List<ClassModel>> FindAllByCourseIdAndPeriodTaskAsync(Guid courseId, int period);

		Task<List<ClassModel>> FindAllByCourseIdTaskAsync(Guid courseId);

		Task<ClassModel> UpdateTaskAsync(ClassModel currentClass, ClassModel newClass);

		Task RemoveAsync(ClassModel @class);
	}
}