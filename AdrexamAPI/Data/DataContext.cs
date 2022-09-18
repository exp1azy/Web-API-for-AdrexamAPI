using Microsoft.EntityFrameworkCore;

namespace AdrexamAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Answers> Answers { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<NavigationItems> NavigationItems { get; set; }
        public DbSet<Questions> Questions { get; set; }

        public DataContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;user=root;password=anton.panin100702;database=gb_adrexam;",
                new MySqlServerVersion(new Version(8, 0, 30))
            );
        }
    }
}
