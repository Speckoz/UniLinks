using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using System.Threading.Tasks;

using UniLink.Client.Site.Services;

namespace UniLink.Client.Site.Components
{
	public partial class Menu
	{
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
		public ThemeService ThemeService { get; private set; }

		[Inject]
		public AuthenticationStateProvider Authentication { get; private set; }

		protected override async Task OnInitializedAsync()
		{
			__isDark = await ThemeService.ChangeSessionThemeAsync();
		}

		private async void ChangeTheme()
		{
			await ThemeService.ChangeTheme(__isDark);
			__isDark = !__isDark;
		}

		private void ToggleNavMenu() => collapseNavMenu = !collapseNavMenu;
	}
}