using Microsoft.EntityFrameworkCore;

using UniLink.API.Models;
using UniLink.Dependencies.Models;

namespace UniLink.API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<CoordinatorModel> Coordinators { get; set; }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<ClassModel> Classes { get; set; }
        public DbSet<DisciplineModel> Disciplines { get; set; }
        public DbSet<LessonModel> Lessons { get; set; }
        public DbSet<StudentModel> Students { get; set; }

        protected DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}