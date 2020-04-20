using System;
using System.Threading.Tasks;
using UniLink.API.Data.VO;
using UniLink.Dependencies.Models;

namespace UniLink.API.Business.Interfaces
{
	public interface ICourseBusiness
	{
		Task<CourseVO> FindByCoordIdTaskAsync(Guid coordId);
	}
}