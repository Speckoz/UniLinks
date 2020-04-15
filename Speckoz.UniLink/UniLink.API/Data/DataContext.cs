using Microsoft.EntityFrameworkCore;

using UniLink.API.Models;
using UniLink.Dependencies.Models;

namespace UniLink.API.Data
{
	public class DataContext : DbContext
	{
		public DbSet<UserLoginModel> Users { get; set; }
		public DbSet<DisciplineModel> Disciplines { get; set; }
		public DbSet<LessonModel> Lessons { get; set; }

		protected DataContext()
		{
		}

		public DataContext(DbContextOptions options) : base(options)
		{
		}
	}
}