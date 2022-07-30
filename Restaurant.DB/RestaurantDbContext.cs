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
        public DbSet<RecipeElement> Recipes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderElement>()
                .HasKey(x => new { x.OrderId, x.MealId });

            modelBuilder.Entity<User>()
                .HasOne(x => x.City)
                .WithMany(x => x.Users);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.User);

            modelBuilder.Entity<Meal>()
                .HasOne(x => x.MealCategory)
                .WithMany(x => x.Meals);

            modelBuilder.Entity<Meal>()
                .HasMany(x => x.RecipeElements)
                .WithOne(x => x.Meal);

            modelBuilder.Entity<Meal>()
                .HasMany(x => x.OrderElements)
                .WithOne(x => x.Meal);

            modelBuilder.Entity<MealCategory>()
                .HasMany(x => x.Meals)
                .WithOne(x => x.MealCategory);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.User)
                .WithMany(x => x.Orders);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.City)
                .WithMany(x => x.Orders);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Promotion)
                .WithMany(x => x.Orders);

            modelBuilder.Entity<Order>()
                .HasMany(x => x.OrderElements)
                .WithOne(x => x.Order);

            modelBuilder.Entity<OrderElement>()
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderElements);

            modelBuilder.Entity<OrderElement>()
                .HasOne(x => x.Meal)
                .WithMany(x => x.OrderElements);

            modelBuilder.Entity<Promotion>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.Promotion);

            modelBuilder.Entity<RecipeElement>()
                .HasOne(x => x.Meal)
                .WithMany(x => x.RecipeElements);

            modelBuilder.Entity<RecipeElement>()
                .HasOne(x => x.Ingredient)
                .WithMany(x => x.RecipeElements);

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(x => x.Email).IsRequired(true).HasMaxLength(320);
                entity.Property(x => x.Password).IsRequired(true).HasMaxLength(255);
                entity.Property(x => x.Role).IsRequired(true).HasColumnType("tinyint");
                entity.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
                entity.Property(x => x.Name).IsRequired(false).HasMaxLength(127);
                entity.Property(x => x.Surname).IsRequired(false).HasMaxLength(127);
                entity.Property(x => x.CityId).IsRequired(false);
                entity.Property(x => x.Address).IsRequired(false).HasMaxLength(255);
                entity.Property(x => x.PhoneNumber).IsRequired(false).HasMaxLength(32);
                entity.Property(x => x.Inserted).IsRequired(true).HasColumnType("smalldatetime");
                entity.Property(x => x.Updated).IsRequired(true).HasColumnType("smalldatetime");
            });

            modelBuilder.Entity<Meal>(entity =>
            {
                entity.Property(x => x.Name).IsRequired(true).HasMaxLength(127);
                entity.Property(x => x.Price).IsRequired(true).HasColumnType("money");
                entity.Property(x => x.Available).IsRequired(true).HasDefaultValue(false);
            });
            
            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(x => x.Name).IsRequired(true).HasMaxLength(127);
                entity.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(false);
            });

            modelBuilder.Entity<MealCategory>(entity =>
            {
                entity.Property(x => x.Name).IsRequired(true).HasMaxLength(127);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(x => x.UserId).IsRequired(true);
                entity.Property(x => x.CityId).IsRequired(true);
                entity.Property(x => x.Name).IsRequired(true).HasMaxLength(127);
                entity.Property(x => x.Surname).IsRequired(true).HasMaxLength(127);
                entity.Property(x => x.Address).IsRequired(true).HasMaxLength(255);
                entity.Property(x => x.Status).IsRequired(true).HasColumnType("tinyint");
                entity.Property(x => x.PromotionCodeId).IsRequired(false);
                entity.Property(x => x.OrderDate).IsRequired(true).HasColumnType("smalldatetime");
            });

            modelBuilder.Entity<OrderElement>(entity =>
            {
                entity.Property(x => x.MealId).IsRequired(true);
                entity.Property(x => x.CurrentPrice).IsRequired(true).HasColumnType("money");
                entity.Property(x => x.Amount).IsRequired(true);
            });
            
            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.Property(x => x.Code).IsRequired(true).HasMaxLength(255);
                entity.Property(x => x.DiscountPercentage).IsRequired(true);
                entity.Property(x => x.StartDate).IsRequired(true).HasColumnType("smalldatetime");
                entity.Property(x => x.EndDate).IsRequired(true).HasColumnType("smalldatetime");
                entity.Property(x => x.IsManuallyDisabled).IsRequired(true).HasDefaultValue(false);
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.Property(x => x.Name).IsRequired(true).HasMaxLength(127);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
