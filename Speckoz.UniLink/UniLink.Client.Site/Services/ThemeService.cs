using Blazored.SessionStorage;

using Microsoft.JSInterop;

using System.Threading.Tasks;

namespace UniLink.Client.Site.Services
{
	public class ThemeService
	{
		private readonly IJSRuntime _runtime;
		private readonly ISessionStorageService _sessionStorage;

		public ThemeService(IJSRuntime runtime, ISessionStorageService sessionStorage)
		{
			_runtime = runtime;
			_sessionStorage = sessionStorage;
		}

		public async Task ChangeTheme(bool isDark)
		{
			await _runtime.InvokeVoidAsync($"ChangeTo{(isDark ? "Dark" : "Light")}");
			await _sessionStorage.SetItemAsync("theme", isDark);
		}

		public async Task<bool> ChangeSessionThemeTaskAsync()
		{
			if(await _sessionStorage.GetItemAsync<bool?>("theme") is bool theme)
			{
				await ChangeTheme(theme);
				return theme;
			}

			return false;
		}
	}
}