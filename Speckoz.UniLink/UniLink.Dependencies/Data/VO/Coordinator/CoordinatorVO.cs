using System;

namespace UniLink.Dependencies.Data.VO.Coordinator
{
	public class CoordinatorVO
	{
		public Guid CoordinatorId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Token { get; set; }
	}
}