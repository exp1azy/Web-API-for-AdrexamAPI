using Microsoft.EntityFrameworkCore;

namespace AdrexamAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Answers> Answers { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<NavigationItems> NavigationItems { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Tokens> Tokens { get; set; }

        private readonly IConfiguration _config;
        public DataContext(IConfiguration config)
        {
            _config = config;
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                _config["ConnectionString"],
                new MySqlServerVersion(_config["MySqlVersion"])
            );
        }
    }
}
