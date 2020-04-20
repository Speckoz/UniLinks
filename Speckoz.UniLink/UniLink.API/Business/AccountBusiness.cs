using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.API.Repository.Interfaces;
using UniLink.API.Services;
using UniLink.Dependencies.Models;
using UniLink.Dependencies.Models.Auxiliary;

namespace UniLink.API.Business
{
	public class AccountBusiness : IAccountBusiness
	{
		private readonly IAccountRepository _accountRepository;
		private readonly GenerateTokenService _tokenService;

		public AccountBusiness(IAccountRepository accountRepository, GenerateTokenService tokenService)
		{
			_accountRepository = accountRepository;
			_tokenService = tokenService;
		}

		public async Task<UserModel> AuthAccountTaskAsync(LoginRequestModel login)
		{
			login.Password = SecurityService.EncryptToSHA256(login.Password);

			return await _accountRepository.FindUserByLoginTaskAsync(login) is UserModel userBase ? ReturnToken(userBase) : (default);
		}

		public async Task<UserModel> AuthUserTaskAsync(string email) =>
			await _accountRepository.FindByEmailTaskAsync(email) is UserModel userBase ? ReturnToken(userBase) : (default);

		private UserModel ReturnToken(UserModel userBase)
		{
			var user = userBase.ToUserModel();
			user.Token = _tokenService.Generate(userBase);
			return user;
		}
	}
}