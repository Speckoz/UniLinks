using UniLink.Dependencies.Attributes;
using UniLink.Dependencies.Enums;

namespace UniLink.Client.Site.Pages.Admin
{
	[Authorizes(UserTypeEnum.Coordinator)]
	public partial class IndexAdminPage
	{
	}
}