using System;
using System.Threading.Tasks;

using UniLink.API.Models;

namespace UniLink.API.Business.Interfaces
{
	public interface IClassBusiness
	{
		Task<ClassModel> AddTasAsync(ClassModel @class);

		Task<ClassModel> FindByClassIdTaskAsync(Guid classId);

		Task RemoveAsync(ClassModel @class);
	}
}