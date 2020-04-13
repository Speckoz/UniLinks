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
			if (await _accountRepository.AuthAccountTaskAsync(login) is UserModel user)
			{
				user.Token = _tokenService.Generate(user);

				return user;
			}

			return null;
		}
	}
}