using System.Threading.Tasks;

using UniLink.Dependencies.Models;

namespace UniLink.API.Business.Interfaces
{
	public interface IUserBusiness
	{
		Task<UserModel> AuthUserTaskAsync(string email);
	}
}