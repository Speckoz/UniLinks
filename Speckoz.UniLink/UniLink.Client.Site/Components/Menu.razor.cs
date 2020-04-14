using UniLink.Client.Site.Helper;

namespace UniLink.Client.Site.Components
{
	public partial class Menu
	{
		private bool collapseNavMenu = true;

		private string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

		private void ToggleNavMenu() => collapseNavMenu = !collapseNavMenu;

		private void ChangeTheme() => DataHelper.Theme = (DataHelper.Theme == DataHelper.DarkTheme) ? DataHelper.LightTheme : DataHelper.DarkTheme;
	}
}