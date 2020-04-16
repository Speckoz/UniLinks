using Microsoft.AspNetCore.Components;

using System.Threading.Tasks;

using UniLink.Client.Site.Services;
using UniLink.Dependencies.Models;

namespace UniLink.Client.Site.Pages.User
{
	public partial class AuthUserPage
	{
		private string email;

		[Inject]
		public AccountService AccountService { get; private set; }

		[Inject]
		public NavigationManager Navigation { get; private set; }

		private async Task AuthAccountTaskAsync()
		{
			if (await AccountService.AuthAccountTaskAsync(email) is UserModel user)
			{
				Navigation.NavigateTo("/user");
			}
		}
	}
}