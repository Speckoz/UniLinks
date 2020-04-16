using UniLink.Dependencies.Models;

namespace UniLink.API.Models
{
	public class UserLoginModel : UserBaseModel
	{
		public string Password { get; set; }

		public UserBaseModel ToUserBaseModel() =>
			new UserBaseModel
			{
				UserId = UserId,
				Name = Name,
				Email = Email,
				UserType = UserType
			};
	}
}