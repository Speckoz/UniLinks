using System.Threading.Tasks;

using UniLink.API.Models;
using UniLink.Dependencies.Models;
using UniLink.Dependencies.Models.Auxiliary;

namespace UniLink.API.Repository.Interfaces
{
	public interface IAccountRepository
	{
		Task<UserBaseModel> FindUserByLoginTaskAsync(LoginRequestModel login);

		Task<UserBaseModel> FindByEmailTaskAsync(string email);

		Task<bool> ExistsByEmailTaskAsync(string email);

		Task<UserLoginModel> AddTaskAsync(UserLoginModel newUser);
	}
}