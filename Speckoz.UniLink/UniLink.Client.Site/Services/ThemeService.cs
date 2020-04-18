using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace UniLink.Client.Site.Services
{
	public class ThemeService
	{
		private readonly IJSRuntime _runtime;

		public ThemeService(IJSRuntime runtime) => 
			_runtime = runtime;

		public void ChangeTheme() => 
			_runtime.InvokeVoidAsync("toggleDarkLight");
	}
}