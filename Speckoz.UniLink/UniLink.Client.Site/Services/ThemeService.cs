using Microsoft.AspNetCore.Components;

namespace UniLink.Client.Site.Services
{
	public class ThemeService
	{
		public const string DarkTheme = "background-color: #272727; color: white;";

		public const string LightTheme = "background-color: white; color: black;";

		[Inject]
		public NavigationManager Navigation { get; private set; }

		public string Theme { get; set; } = DarkTheme;

		public void ChangeTheme()
		{
			Theme = (Theme == DarkTheme) ? LightTheme : DarkTheme;
			//Navigation.NavigateTo(Navigation.Uri, true);
		}
	}
}