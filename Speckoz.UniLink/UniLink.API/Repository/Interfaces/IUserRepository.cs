using System.Threading.Tasks;

using UniLink.Dependencies.Models;

namespace UniLink.API.Repository.Interfaces
{
	public interface IUserRepository
	{
		Task<UserBaseModel> FindByEmailTaskAsync(string email);
	}
}