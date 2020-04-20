using System.Threading.Tasks;
using UniLink.API.Data.VO;
using UniLink.Dependencies.Models;
using UniLink.Dependencies.Models.Auxiliary;

namespace UniLink.API.Business.Interfaces
{
	public interface IAccountBusiness
	{
		Task<UserVO> AuthAccountTaskAsync(LoginRequestModel login);
		Task<UserVO> AuthUserTaskAsync(string email);
	}
}