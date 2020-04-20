using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.API.Data.Converters;
using UniLink.API.Models;
using UniLink.API.Repository.Interfaces;
using UniLink.API.Services;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Enums;
using UniLink.Dependencies.Models.Auxiliary;

namespace UniLink.API.Business
{
	public class CoordinatorBusiness : ICoordinatorBusiness
	{
		private readonly ICoordinatorRepository _accountRepository;
		private readonly GenerateTokenService _tokenService;
		private readonly CoordinatorConverter _coordinatorConverter;

		public CoordinatorBusiness(ICoordinatorRepository accountRepository, GenerateTokenService tokenService)
		{
			_accountRepository = accountRepository;
			_tokenService = tokenService;
			_coordinatorConverter = new CoordinatorConverter();
		}

		public async Task<CoordinatorVO> AuthAccountTaskAsync(LoginRequestModel login)
		{
			login.Password = SecurityService.EncryptToSHA256(login.Password);

			if (await _accountRepository.FindUserByLoginTaskAsync(login) is CoordinatorModel user)
			{
				CoordinatorVO userVO = _coordinatorConverter.Parse(user);
				userVO.Token = _tokenService.Generate(user.CoordinatorId, UserTypeEnum.Coordinator);

				return userVO;
			}

			return default;
		}
	}
}