using UniLinks.API.Data;

namespace UniLinks.API.Repository
{
    public abstract class BaseRepository
    {
        protected readonly DataContext _context;

        protected BaseRepository(DataContext context) => _context = context;
    }
}