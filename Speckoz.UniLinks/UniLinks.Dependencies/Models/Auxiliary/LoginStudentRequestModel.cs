using System.ComponentModel.DataAnnotations;

namespace UniLinks.Dependencies.Models.Auxiliary
{
	public class LoginStudentRequestModel
	{
		[Required]
		public string Email { get; set; }
	}
}