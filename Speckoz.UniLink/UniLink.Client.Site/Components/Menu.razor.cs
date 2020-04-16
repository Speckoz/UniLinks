namespace UniLink.Client.Site.Components
{
	public partial class Menu
	{
		private bool collapseNavMenu = true;

		private string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

		private void ToggleNavMenu() => collapseNavMenu = !collapseNavMenu;
	}
}