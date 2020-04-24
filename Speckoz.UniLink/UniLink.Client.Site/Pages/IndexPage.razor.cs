using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace UniLink.Client.Site.Pages
{
	public partial class IndexPage
	{
		[Inject]
		private AuthenticationStateProvider Authentication { get; set; }
	}
}