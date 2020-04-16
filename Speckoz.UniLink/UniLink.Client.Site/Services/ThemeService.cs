using Microsoft.AspNetCore.Components;

namespace UniLink.Client.Site.Services
{
	public class ThemeService
	{
		private readonly NavigationManager _navigation;

		public const string DarkTheme = "background-color: #333333; color: white;";
		public const string LightTheme = "background-color: white; color: black;";

		public string Theme { get; set; } = LightTheme;

		public ThemeService(NavigationManager navigation) =>
			_navigation = navigation;

		public void ChangeTheme()
		{
			Theme = (Theme == DarkTheme) ? LightTheme : DarkTheme;
			_navigation.NavigateTo(_navigation.Uri, true);
		}
	}
}