using UniLink.API.Data;

namespace UniLink.API.Repository
{
    public abstract class BaseRepository
    {
        protected readonly DataContext _context;

        protected BaseRepository(DataContext context) => _context = context;
    }
}