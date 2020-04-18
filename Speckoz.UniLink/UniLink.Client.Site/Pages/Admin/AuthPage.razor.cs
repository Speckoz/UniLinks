using Microsoft.AspNetCore.Components;

using System.Threading.Tasks;

using UniLink.Client.Site.Services;
using UniLink.Dependencies.Models;
using UniLink.Dependencies.Models.Auxiliary;

namespace UniLink.Client.Site.Pages.Admin
{
	public partial class AuthPage
	{
		private string email;
		private string password;

		[Inject]
		public AccountService AccountService { get; private set; }

		[Inject]
		public NavigationManager Navigation { get; private set; }

		private async Task AuthAccountTaskAsync()
		{
			if (await AccountService.AuthAccountTaskAsync(new LoginRequestModel { Email = email, Password = password }) is UserModel user)
			{
				Navigation.NavigateTo("/admin");
			}
		}
	}
}