using FinWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace FinWebMvc.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Category> Categories { get; set; }
        public DbSet<Record> Records { get; set; }

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("Sqlite"));
        }
    }
}
