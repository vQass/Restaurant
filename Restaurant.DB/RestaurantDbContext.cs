using Microsoft.EntityFrameworkCore;
using Restaurant.DB.Entities;

namespace Restaurant.DB
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealCategory> MealsCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderElement> OrdersElements { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UsersDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderElement>().HasKey(x => new { x.Id, x.MealId });
            modelBuilder.Entity<Recipe>().HasKey(x => new { x.MealId, x.IngredientId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
