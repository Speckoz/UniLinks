using UniLink.API.Data;

namespace UniLink.API.Repository
{
	public abstract class Repository
	{
		protected readonly DataContext _context;

		protected Repository(DataContext context) => _context = context;
	}
}