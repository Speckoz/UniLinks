using System.Threading.Tasks;

namespace UniLink.Client.Site.Services.Interfaces
{
	public interface IAuthService
	{
		Task AuthorizeAsync();
	}
}