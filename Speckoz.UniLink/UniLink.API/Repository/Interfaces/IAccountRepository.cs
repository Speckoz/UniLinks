using System.Threading.Tasks;

using UniLink.Dependencies.Models;
using UniLink.Dependencies.Models.Auxiliary;

namespace UniLink.API.Repository.Interfaces
{
	public interface IAccountRepository
	{
		Task<UserBaseModel> AuthAccountTaskAsync(LoginRequestModel login);
	}
}