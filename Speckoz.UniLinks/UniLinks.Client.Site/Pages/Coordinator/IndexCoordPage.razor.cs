using UniLinks.Dependencies.Attributes;
using UniLinks.Dependencies.Enums;

namespace UniLinks.Client.Site.Pages.Admin
{
    [Authorizes(UserTypeEnum.Coordinator)]
    public partial class IndexCoordPage
    {
    }
}