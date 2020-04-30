using System;
using System.Threading.Tasks;

using UniLink.Dependencies.Data.VO;

namespace UniLink.API.Business.Interfaces
{
	public interface IClassBusiness
	{
		Task<ClassVO> AddTasAsync(ClassVO @class);

		Task<ClassVO> FindByClassIdTaskAsync(Guid classId);

		Task<ClassVO> FindByURITaskAsync(string uri);

		Task RemoveAsync(ClassVO @class);
	}
}