using Microsoft.EntityFrameworkCore;
using Restaurant.Entities.Entities;

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
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderElement>()
                .HasKey(x => new { x.OrderId, x.MealId });

            modelBuilder.Entity<Meal>()
                .HasOne(x => x.MealCategory)
                .WithMany(x => x.Meals)
                .HasForeignKey(x => x.MealCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Meal>()
                .HasMany(x => x.Ingredients)
                .WithMany(x => x.Meals)
                .UsingEntity(x => x.ToTable("Recipes"));

            modelBuilder.Entity<Order>()
                .HasOne(x => x.User)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<City>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.City)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.City)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderElement>()
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderElements)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderElement>()
                .HasOne(x => x.Meal)
                .WithMany(x => x.OrderElements)
                .HasForeignKey(x => x.MealId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(x => x.Email).IsRequired(true).HasMaxLength(320);
                entity.Property(x => x.Password).IsRequired(true).HasMaxLength(255);
                entity.Property(x => x.Role).IsRequired(true).HasColumnType("tinyint");
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

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.Property(x => x.Name).IsRequired(true).HasMaxLength(127);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
