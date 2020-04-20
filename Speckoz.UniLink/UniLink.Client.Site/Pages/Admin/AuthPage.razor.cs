﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using System.Threading.Tasks;

using UniLink.Client.Site.Services;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Models.Auxiliary;

namespace UniLink.Client.Site.Pages.Admin
{
	public partial class AuthPage
	{
		private string email;
		private string password;
		private string show = "collapse";

		[Inject]
		private AccountService AccountService { get; set; }

		[Inject]
		private AuthenticationStateProvider Authentication { get; set; }

		[Inject]
		private NavigationManager Navigation { get; set; }

		private async Task AuthAccountTaskAsync()
		{
			if (await AccountService.AuthAccountTaskAsync(new LoginRequestModel { Email = email, Password = password }) is CoordinatorVO coord)
			{
				await ((AuthenticationStateProviderService) Authentication).MarkUserWithAuthenticatedAsync(coord.Token);
				Navigation.NavigateTo("/admin");
			}
			else
				show = nameof(show);
		}

		private void HideAlert() => show = "collapse";
	}
}