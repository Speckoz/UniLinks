using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

using UniLink.API.Data;
using UniLink.API.Repository.Interfaces;
using UniLink.Dependencies.Enums;
using UniLink.Dependencies.Models;

namespace UniLink.API.Repository
{
	public class UserRepository : Repository, IUserRepository
	{
		public UserRepository(DataContext context) : base(context)
		{
		}

		public async Task<UserBaseModel> FindByEmailTaskAsync(string email) =>
			await _content.Users.SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower() && x.UserType == UserTypeEnum.Student);
	}
}