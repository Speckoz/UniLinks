using Microsoft.AspNetCore.Components;

using UniLink.Client.Site.Helper;

namespace UniLink.Client.Site.Components
{
	public partial class Menu
	{
		private bool collapseNavMenu = true;

		private string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

		[Inject]
		public NavigationManager Navigation { get; private set; }

		private void ToggleNavMenu() => collapseNavMenu = !collapseNavMenu;

		private void ChangeTheme()
		{
			DataHelper.Theme = (DataHelper.Theme == DataHelper.DarkTheme) ? DataHelper.LightTheme : DataHelper.DarkTheme;
			Navigation.NavigateTo(Navigation.Uri, true);
		}
	}
}