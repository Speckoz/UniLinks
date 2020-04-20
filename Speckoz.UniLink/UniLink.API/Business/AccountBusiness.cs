using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.API.Data.Converters;
using UniLink.API.Data.VO;
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
		private readonly UserConverter _converter;

		public AccountBusiness(IAccountRepository accountRepository, GenerateTokenService tokenService)
		{
			_accountRepository = accountRepository;
			_tokenService = tokenService;
			_converter = new UserConverter();
		}

		public async Task<UserVO> AuthAccountTaskAsync(LoginRequestModel login)
		{
			login.Password = SecurityService.EncryptToSHA256(login.Password);

			var user = await _accountRepository.FindUserByLoginTaskAsync(login);
			if (user is UserModel)
			{
				return _converter.Parse(user);
			}
			return default;
		}

		public async Task<UserVO> AuthUserTaskAsync(string email)
		{
			var user = await _accountRepository.FindByEmailTaskAsync(email);
			if (user is UserModel)
			{
				return _converter.Parse(user);
			}

			return default;
		}
	}
}