using System;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO.Coordinator;
using UniLinks.Dependencies.Models.Auxiliary;

namespace UniLinks.API.Business.Interfaces
{
	public interface ICoordinatorBusiness
	{
		Task<AuthCoordinatorVO> AuthAccountTaskAsync(LoginRequestModel login);

		Task<AuthCoordinatorVO> FindByCoordIdTaskAsync(Guid coordId);
	}
}