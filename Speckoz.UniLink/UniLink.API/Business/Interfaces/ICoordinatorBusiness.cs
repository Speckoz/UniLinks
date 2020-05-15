using System.Threading.Tasks;

using UniLink.Dependencies.Data.VO.Coordinator;
using UniLink.Dependencies.Models.Auxiliary;

namespace UniLink.API.Business.Interfaces
{
	public interface ICoordinatorBusiness
	{
		Task<AuthCoordinatorVO> AuthAccountTaskAsync(LoginRequestModel login);
	}
}