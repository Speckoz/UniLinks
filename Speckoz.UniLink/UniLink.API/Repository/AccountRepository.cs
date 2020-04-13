using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

using UniLink.API.Data;
using UniLink.API.Repository.Interfaces;
using UniLink.Dependencies.Enums;
using UniLink.Dependencies.Models;
using UniLink.Dependencies.Models.Auxiliary;

namespace UniLink.API.Repository
{
	public class AccountRepository : Repository, IAccountRepository
	{
		public AccountRepository(DataContext context) : base(context)
		{
		}

		public async Task<UserBaseModel> AuthAccountTaskAsync(LoginRequestModel login) => 
			await _content.Users.FirstOrDefaultAsync(x => x.Email == login.Email && x.Password == login.Password && x.UserType == UserTypeEnum.Coordinator);
	}
}