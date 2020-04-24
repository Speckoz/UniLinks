using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using System.Threading.Tasks;

using UniLink.Client.Site.Services;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Data.VO.Student;

namespace UniLink.Client.Site.Pages.Student
{
	public partial class AuthStudentPage
	{
		private string email;
		private string show = "collapse";

		[Inject]
		private AccountService AccountService { get; set; }

		[Inject]
		private AuthenticationStateProvider Authentication { get; set; }

		[Inject]
		private NavigationManager Navigation { get; set; }

		private async Task AuthAccountTaskAsync()
		{
			if (await AccountService.AuthAccountTaskAsync(email) is StudentVO user)
			{
				await ((AuthenticationStateProviderService) Authentication).MarkUserWithAuthenticatedAsync(user.Token);
				Navigation.NavigateTo("/student");
			}
			else
				show = "show";
		}

		private void HideAlert()
		{
			show = "collapse";
		}
	}
}