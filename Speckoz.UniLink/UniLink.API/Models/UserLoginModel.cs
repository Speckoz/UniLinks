using UniLink.Dependencies.Models;

namespace UniLink.API.Models
{
	public class UserLoginModel : UserBaseModel
	{
		public string Password { get; set; }
	}
}