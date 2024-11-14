using Databases.Entities;
using Microsoft.EntityFrameworkCore;
using Version = Databases.Entities.Version;

namespace Databases.DbContexts
{
    public class ConfigurationsDbContext : DbContext
    {
        public ConfigurationsDbContext(DbContextOptions<ConfigurationsDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<Configuration> Configurations { get; set; }

        public DbSet<Version> Versions { get; set; }

        public DbSet<HotKey> HotKeys { get; set; }

        public DbSet<Command> Commands { get; set; }

        public DbSet<User> Users { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //}
    }
}
