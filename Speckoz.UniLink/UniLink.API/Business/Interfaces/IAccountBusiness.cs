using System.Threading.Tasks;

using UniLink.Dependencies.Models;
using UniLink.Dependencies.Models.Auxiliary;

namespace UniLink.API.Business.Interfaces
{
	public interface IAccountBusiness
	{
		Task<UserModel> AuthAccountTaskAsync(LoginRequestModel login);
	}
}