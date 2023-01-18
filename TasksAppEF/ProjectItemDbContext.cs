using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProgressApp
{
    public class ProjectItemDbContext : DbContext
    {
        public DbSet<UserDto> Users { get; set; }
        public DbSet<ProjectItemDto> ProjectItems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=.\ProgressApp.db");
        }
        public void EnsureCreated()
        {
            Database.EnsureCreated();
        }
    }
    public class ProjectItemDbContextFactory : IDesignTimeDbContextFactory<ProjectItemDbContext>
    {
        public ProjectItemDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProjectItemDbContext>();
            optionsBuilder.UseSqlite(@"Data Source=.\ProgressApp.db");

            return new ProjectItemDbContext();
        }
    }
}
