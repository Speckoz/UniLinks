using System.ComponentModel.DataAnnotations;

namespace UniLink.API.Models.Auxiliary
{
	public class EmailFromBody
	{
		[Required]
		public string Email { get; set; }
	}
}