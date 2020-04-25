using Microsoft.AspNetCore.Components;

using System.Threading.Tasks;

using UniLink.Client.Site.Services;

namespace UniLink.Client.Site.Pages.Student
{
	public partial class AuthStudentPage
	{
		private string email;
		private string show = "collapse";

		[Inject]
		private AccountService AccountService { get; set; }

		[Inject]
		private NavigationManager Navigation { get; set; }

		private async Task AuthAccountTaskAsync()
		{
			if (await AccountService.AuthAccountTaskAsync(email))
				Navigation.NavigateTo("/student");
			else
				show = "show";
		}

		private void HideAlert()
		{
			show = "collapse";
		}
	}
}