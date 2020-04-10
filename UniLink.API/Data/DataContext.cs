using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniLink.API.Models;

namespace UniLink.API.Data
{
    public class DataContext : DbContext
    {
        // Tables
        public DbSet<UserModel> Users { get; set; }

        protected DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}
