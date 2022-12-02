using Microsoft.AspNetCore.Identity;
using Restaurant.Business.IServices;
using Restaurant.DB;
using Restaurant.DB.Entities;
using Restaurant.DB.Enums;

namespace Restaurant.Business.Services
{
    public class SeederService : ISeederService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        private readonly string MealCategoryMainMeals = "Dania główne";
        private readonly string MealCategorySoups = "Zupy";

        public SeederService(RestaurantDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Cities.Any())
                {
                    var cities = GetCities();
                    _dbContext.Cities.AddRange(cities);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Users.Any())
                {
                    var users = GetUsersWithDetails();
                    _dbContext.Users.AddRange(users);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.MealsCategories.Any())
                {
                    var categotries = GetMealsCategries();
                    _dbContext.MealsCategories.AddRange(categotries);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Meals.Any())
                {
                    var meals = GetMeals();
                    _dbContext.Meals.AddRange(meals);
                    _dbContext.SaveChanges();

                    FillMealRecipe();
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Orders.Any())
                {
                    var orders = GetOrders();
                    _dbContext.Orders.AddRange(orders);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Ingredients.Any())
                {
                    var ingredients = GetIngredients();
                    _dbContext.Ingredients.AddRange(ingredients);
                    _dbContext.SaveChanges();
                }
            }
        }

        #region Cities

        private IEnumerable<City> GetCities()
        {
            var cities = new List<City>() {
                new City()
                {
                    Name = "Gliwice"
                },
                new City()
                {
                    Name = "Zabrze"
                },
                new City()
                {
                    Name = "Katowice"
                }
            };

            return cities;
        }

        #endregion

        #region Users

        private IEnumerable<User> GetUsersWithDetails()
        {
            var date = DateTime.Now;
            var city = _dbContext.Cities.FirstOrDefault();

            var users = new List<User>()
            {
                new User()
                {
                    Email = "test1@test",
                    Password = "pass123",
                    Role = RoleEnum.HeadAdmin,
                    City = city,
                    Address = "Testowa 1",
                    Name = "Patryk",
                    Surname = "Zub",
                    PhoneNumber = "123456789",
                    Inserted = date,
                    Updated = date
                },
                new User()
                {
                    Email = "test2@test",
                    Password = "pass123",
                    Role = RoleEnum.Admin,
                    Inserted = date,
                    Updated = date

                },
                new User()
                {
                    Email = "test3@test",
                    Password = "pass123",
                    Role = RoleEnum.Employee,
                    City =city,
                    Address = "Testowa 3",
                    Name = "Patryk 3",
                    Surname = "Zub 3",
                    PhoneNumber = "987654321",
                    Inserted = date,
                    Updated = date

                },
                new User()
                {
                    Email = "test4@test",
                    Password = "pass123",
                    Role = RoleEnum.User,
                    City = city,
                    Address = "Testowa 4",
                    Name = "Patryk 4",
                    Surname = "Zub 4",
                    PhoneNumber = "987654321",
                    Inserted = date,
                    Updated = date
                },
                new User()
                {
                    Email = "test5@test",
                    Password = "pass123",
                    Role = RoleEnum.User,
                    City =city,
                    Address = "Testowa 5",
                    PhoneNumber = "987654321",
                    Inserted = date,
                    Updated = date
                },
                new User()
                {
                    Email = "test6@test",
                    Password = "pass123",
                    Role = RoleEnum.User,
                    Inserted = date,
                    Updated = date
                }
            };

            foreach (var user in users)
            {
                user.Password = _passwordHasher.HashPassword(user, user.Password);
            }

            return users;
        }

        #endregion

        #region MealsCategries

        public IEnumerable<MealCategory> GetMealsCategries()
        {
            var categories = new List<MealCategory>()
            {
                new MealCategory() { Name = MealCategoryMainMeals },
                new MealCategory() { Name = MealCategorySoups }
            };
            return categories;
        }

        #endregion

        #region Meals

        private IEnumerable<Meal> GetMeals()
        {
            var meals = new List<Meal>()
            {
                new Meal
                {
                    Name = "Kotlet z piersi kurczaka",
                    MealCategory = _dbContext.MealsCategories.FirstOrDefault(x => x.Name == MealCategoryMainMeals),
                    Price = 10.4m,
                    Available = true,
                },
                new Meal
                {
                    Name = "Rosół",
                    MealCategory = _dbContext.MealsCategories.FirstOrDefault(x => x.Name == MealCategorySoups),
                    Price = 6.4m,
                    Available = true,
                },
                new Meal
                {
                    Name = "Kwaśnica",
                    MealCategory = _dbContext.MealsCategories.FirstOrDefault(x => x.Name == MealCategorySoups),
                    Price = 17.4m,
                    Available = true,
                }
            };
            return meals;
        }

        #endregion

        #region Orders

        private IEnumerable<Order> GetOrders()
        {
            var city = _dbContext.Cities.FirstOrDefault();
            var meal1 = _dbContext.Meals.FirstOrDefault();
            var meal2 = _dbContext.Meals.OrderBy(x => x.Id).LastOrDefault();

            var orders = new List<Order>()
            {
                new Order
                {
                    User = _dbContext.Users.OrderBy(x => x.Id).LastOrDefault(x => x.Role == RoleEnum.User),
                    OrderDate = DateTime.Now,
                    Address = "Długa 25",
                    City = city,
                    PhoneNumber = "123456789",
                    Name = "Patryk",
                    Surname = "Zub",
                    //Status = OrderStatusEnum.Realizowane,
                    OrderElements = new List<OrderElement>()
                    {
                        new OrderElement
                        {
                            Meal = meal1,
                            Amount = 3,
                            CurrentPrice = meal1.Price
                        },
                        new OrderElement
                        {
                            Meal = meal2,
                            Amount = 1,
                            CurrentPrice = meal2.Price
                        }
                    }
                },
                new Order
                {
                    User = _dbContext.Users.FirstOrDefault(x => x.Role == RoleEnum.User),
                    OrderDate = DateTime.Now,
                    Address = "Testowa 13/2",
                    City = city,
                    PhoneNumber = "987654321",
                    Name = "Adam",
                    Surname = "Nowak",
                    //Status = OrderStatusEnum.Realizowane,
                    OrderElements = new List<OrderElement>()
                    {
                        new OrderElement
                        {
                            Meal = meal2,
                            Amount = 1,
                            CurrentPrice = meal2.Price
                        },
                        new OrderElement
                        {
                            Meal = meal1,
                            Amount = 6,
                            CurrentPrice = meal1.Price
                        }
                    }
                }
            };

            return orders;
        }

        #endregion

        #region Ingredients

        private IEnumerable<Ingredient> GetIngredients()
        {
            var ingredients = new List<Ingredient>()
            {
                new Ingredient
                {
                    Name = "Pierś z kurczaka"
                },
                new Ingredient
                {
                    Name = "Bułka tarta"
                },
                new Ingredient
                {
                    Name = "Jajka"
                },
                new Ingredient
                {
                    Name = "Kura"
                },
                new Ingredient
                {
                    Name = "Marchewka"
                },
                new Ingredient
                {
                    Name = "Pietruszka"
                },
                new Ingredient
                {
                    Name = "Cebula"
                },
                new Ingredient
                {
                    Name = "Seler"
                },
                new Ingredient
                {
                    Name = "Czosnek"
                },
                new Ingredient
                {
                    Name = "Liść laurowy"
                },
                new Ingredient
                {
                    Name = "Ziele angielskie"
                },
                new Ingredient
                {
                    Name = "Pieprz"
                },
                new Ingredient
                {
                    Name = "Sól"
                },
                new Ingredient
                {
                    Name = "Żeberka wędzone"
                },
                new Ingredient
                {
                    Name = "Boczek"
                },
                new Ingredient
                {
                    Name = "Kminek"
                },
                new Ingredient
                {
                    Name = "Ziemniaki"
                },
                new Ingredient
                {
                    Name = "Kiszona kapusta"
                },
                new Ingredient
                {
                    Name = "Majeranek"
                }

            };
            return ingredients;
        }

        #endregion

        #region Recipe 

        private void FillMealRecipe()
        {
            var meals = _dbContext.Meals.ToList();
            var ingredients = _dbContext.Ingredients.ToList();

            var firstMeal = meals.FirstOrDefault(x => x.Name == "Kotlet z piersi kurczaka");
            var firstMealIngredients = ingredients.Where(x =>
                    x.Name == "Pierś z kurczaka"
                    || x.Name == "Bułka tarta"
                    || x.Name == "Jajka"
                    || x.Name == "Sól"
                    || x.Name == "Pieprz")
                .ToList();
            firstMeal.Ingredients.AddRange(firstMealIngredients);

            var secondMeal = meals.FirstOrDefault(x => x.Name == "Rosół");
            var secondMealIngredients = ingredients.Where(x =>
                    x.Name == "Kura"
                    || x.Name == "Pietruszka"
                    || x.Name == "Marchewka"
                    || x.Name == "Cebula"
                    || x.Name == "Czosnek"
                    || x.Name == "Ziele angielskie"
                    || x.Name == "Pieprz"
                    || x.Name == "Liść laurowy"
                    || x.Name == "Sól"
                    || x.Name == "Seler")
                .ToList();
            secondMeal.Ingredients.AddRange(secondMealIngredients);

            var thirdMeal = meals.FirstOrDefault(x => x.Name == "Kwaśnica");
            var thirdMealIngredients = ingredients.Where(x =>
                    x.Name == "Czosnek"
                    || x.Name == "Liść laurowy"
                    || x.Name == "Ziele angielskie"
                    || x.Name == "Pieprz"
                    || x.Name == "Sól"
                    || x.Name == "Żeberka wędzone"
                    || x.Name == "Boczek"
                    || x.Name == "Ziemniakie"
                    || x.Name == "Kminek"
                    || x.Name == "Majeranek"
                    || x.Name == "Kiszona kapusta"
                    )
                .ToList();
            thirdMeal.Ingredients.AddRange(thirdMealIngredients);

        }

        #endregion

    }
}
