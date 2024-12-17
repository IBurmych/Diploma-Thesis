using Diploma_Thesis.Entities;
using Microsoft.EntityFrameworkCore;

namespace Diploma_Thesis.Repositories
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Expertise> Expertises { get; set; }
        public DbSet<Vector> Vectors { get; set; }
        public DbSet<Diapason> Diapasons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // todo: fluent api settings
        }
    }
}
