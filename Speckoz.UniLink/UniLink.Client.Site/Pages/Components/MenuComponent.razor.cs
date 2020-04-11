using UniLink.Client.Site.Helper;

namespace UniLink.Client.Site.Pages.Components
{
	public partial class MenuComponent
	{
		private bool collapseNavMenu = true;

		private string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

		private void ToggleNavMenu() => collapseNavMenu = !collapseNavMenu;

		private void ChangeTheme() => DataHelper.Theme = (DataHelper.Theme == DataHelper.DarkTheme) ? DataHelper.LightTheme : DataHelper.DarkTheme;
	}
}