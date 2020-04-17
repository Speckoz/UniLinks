using System.Threading.Tasks;

using UniLink.Dependencies.Models;
using UniLink.Dependencies.Models.Auxiliary;

namespace UniLink.Client.Site.Services.Interfaces
{
	public interface IAuthService
	{
		Task<UserModel> AuthTaskAsync(LoginRequestModel loginModel);

		Task LogoutAsync();
	}
}