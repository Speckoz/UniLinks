using System.ComponentModel.DataAnnotations;

using UniLink.Dependencies.Models;

namespace UniLink.API.Models
{
	public class UserLoginModel : UserBaseModel
	{
		[Required]
		public string Password { get; set; }
	}
}