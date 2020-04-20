using UniLink.Dependencies.Models;

namespace UniLink.API.Models
{
	public class UserLoginModel : UserModel
	{
		public string Password { get; set; }

		public UserModel ToUserBaseModel() =>
			new UserModel
			{
				UserId = UserId,
				Name = Name,
				Email = Email,
				UserType = UserType
			};
	}
}