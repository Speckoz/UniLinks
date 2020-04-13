using System.ComponentModel.DataAnnotations;

namespace UniLink.Dependencies.Models.Auxiliary
{
	public class LoginRequestModel
	{
		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}