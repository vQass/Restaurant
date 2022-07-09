using Microsoft.EntityFrameworkCore;

namespace Restaurant.API.Entities
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrdersDetails { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UsersDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasKey(x => new { x.Id, x.MealId });
            modelBuilder.Entity<Recipes>().HasKey(x => new { x.MealId, x.IngredientId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
