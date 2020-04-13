using UniLink.API.Data;

namespace UniLink.API.Repository
{
	public class Repository
	{
		protected readonly DataContext _content;

		protected Repository(DataContext context) => _content = context;
	}
}