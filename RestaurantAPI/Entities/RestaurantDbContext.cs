using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Entities
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {
        }

        //public DbSet<Article> Articles { get; set; }
        //public DbSet<Administrator> Administrators { get; set; }
        //public DbSet<ArticleContent> ArticleContents { get; set; }
        //public DbSet<Comment> Comments { get; set; }
        //public DbSet<Gallery> Galleries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
