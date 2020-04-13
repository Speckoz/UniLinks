using Microsoft.AspNetCore.Components;

using System.Threading.Tasks;

using UniLink.Client.Site.Services;
using UniLink.Dependencies.Models;
using UniLink.Dependencies.Models.Auxiliary;

namespace UniLink.Client.Site.Pages
{
	public partial class LoginPage
	{
		private string email;
		private string password;
		private string result;

		[Inject]
		public AccountService AccountService { get; private set; }

		private async Task AuthAccountTaskAsync()
		{
			result = default;
			if (await AccountService.AuthAccountTaskAsync(new LoginRequestModel { Email = email, Password = password }) is UserModel user)
			{
				result = "blz";
			}

			result = "nao deu";
		}
	}
}