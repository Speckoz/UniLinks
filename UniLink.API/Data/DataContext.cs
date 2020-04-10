using Microsoft.EntityFrameworkCore;

using UniLink.API.Models;

namespace UniLink.API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<UserLoginModel> Users { get; set; }

        protected DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}