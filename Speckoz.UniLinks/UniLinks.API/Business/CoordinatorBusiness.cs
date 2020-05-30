using System;
using System.Threading.Tasks;

using UniLinks.API.Business.Interfaces;
using UniLinks.API.Data.Converters.Coordinator;
using UniLinks.API.Models;
using UniLinks.API.Repository.Interfaces;
using UniLinks.API.Services;
using UniLinks.Dependencies.Data.VO.Coordinator;
using UniLinks.Dependencies.Enums;
using UniLinks.Dependencies.Models.Auxiliary;

namespace UniLinks.API.Business
{
	public class CoordinatorBusiness : ICoordinatorBusiness
	{
		private readonly ICoordinatorRepository _coordinatorRepository;
		private readonly GenerateTokenService _tokenService;
		private readonly AuthCoordinatorConverter _authCoordinatorConverter;

		public CoordinatorBusiness(ICoordinatorRepository accountRepository, GenerateTokenService tokenService)
		{
			_coordinatorRepository = accountRepository;
			_tokenService = tokenService;
			_authCoordinatorConverter = new AuthCoordinatorConverter();
		}

		public async Task<AuthCoordinatorVO> AuthAccountTaskAsync(LoginRequestModel login)
		{
			login.Password = SecurityService.EncryptToSHA256(login.Password);

			if (await _coordinatorRepository.FindUserByLoginTaskAsync(login) is CoordinatorModel user)
			{
				AuthCoordinatorVO userVO = _authCoordinatorConverter.Parse(user);
				userVO.Token = _tokenService.Generate(user.CoordinatorId, UserTypeEnum.Coordinator);

				return userVO;
			}

			return default;
		}

		public async Task<AuthCoordinatorVO> FindByCoordIdTaskAsync(Guid coordId) =>
			_authCoordinatorConverter.Parse(await _coordinatorRepository.FindByCoordIdTaskAsync(coordId));
	}
}