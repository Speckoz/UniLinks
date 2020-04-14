using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.API.Repository.Interfaces;
using UniLink.API.Services;
using UniLink.Dependencies.Models;

namespace UniLink.API.Business
{
	public class UserBusiness : IUserBusiness
	{
		private readonly IUserRepository _userRepository;
		private readonly GenerateTokenService _tokenService;

		public UserBusiness(IUserRepository userRepository, GenerateTokenService tokenService)
		{
			_userRepository = userRepository;
			_tokenService = tokenService;
		}

		public async Task<UserModel> AuthUserTaskAsync(string email)
		{
			if (await _userRepository.FindByEmailTaskAsync(email) is UserBaseModel userBase)
			{
				var user = userBase.ToUserModel();
				user.Token = _tokenService.Generate(userBase);

				return user;
			}

			return default;
		}
	}
}