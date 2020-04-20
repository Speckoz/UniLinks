using System;
using System.ComponentModel.DataAnnotations;

using UniLink.Dependencies.Enums;

namespace UniLink.Dependencies.Models
{
	public class UserBaseModel
	{
		[Key]
		public Guid UserId { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public UserTypeEnum UserType { get; set; }

		public UserModel ToUserModel() => new UserModel
		{
			UserId = UserId,
			Name = Name,
			Email = Email,
			UserType = UserType
		};
	}
}