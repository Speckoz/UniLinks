using UniLink.API.Data;

namespace UniLink.API.Repository
{
	public abstract class Repository
	{
		protected readonly DataContext _content;

		protected Repository(DataContext context) => _content = context;
	}
}