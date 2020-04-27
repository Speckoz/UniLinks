using Blazored.SessionStorage;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using System.Threading.Tasks;

using UniLink.Client.Site.Services;

namespace UniLink.Client.Site.Components
{
	public partial class Menu
	{
		private string name;
		private bool collapseNavMenu = true;
		private bool __isDark;

		private string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

		private bool IsDark
		{
			get => __isDark;
			set
			{
				__isDark = value;
				ChangeTheme();
			}
		}

		[Inject]
		private ThemeService ThemeService { get; set; }

		[Inject]
		private AuthenticationStateProvider Authentication { get; set; }

		[Inject]
		private ISessionStorageService SessionStorage { get; set; }

		protected override async Task OnInitializedAsync()
		{
			__isDark = await ThemeService.ChangeSessionThemeTaskAsync();
			name = await SessionStorage.GetItemAsync<string>("name");
		}

		private async void ChangeTheme()
		{
			await ThemeService.ChangeTheme(__isDark);
		}

		private void ToggleNavMenu() => collapseNavMenu = !collapseNavMenu;
	}
}