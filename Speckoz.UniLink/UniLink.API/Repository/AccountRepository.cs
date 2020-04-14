using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

using UniLink.API.Data;
using UniLink.API.Models;
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

		public async Task<UserBaseModel> FindUserByLoginTaskAsync(LoginRequestModel login) =>
			await _context.Users.SingleOrDefaultAsync(x => x.Email.ToLower() == login.Email.ToLower() && x.Password == login.Password && x.UserType == UserTypeEnum.Coordinator);

		public async Task<UserBaseModel> FindByEmailTaskAsync(string email) =>
			await _context.Users.SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower() && x.UserType == UserTypeEnum.Student);

		public async Task<bool> ExistsByEmailTaskAsync(string email) => 
			await _context.Users.AnyAsync(x => x.Email == email);

		public async Task<UserLoginModel> AddTaskAsync(UserLoginModel newUser)
		{
			if ((await _context.AddAsync(newUser)).Entity is UserLoginModel user)
			{
				await _context.SaveChangesAsync();
				return user;
			}

			return default;
		}
	}
}