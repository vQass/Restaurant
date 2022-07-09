using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.API.Entities;
using Restaurant.API.Interfaces;
using Restaurant.API.Models.Enums;
using RestaurantAPI.Entities;

namespace Restaurant.API.Seeder
{
    public class Seeder : ISeeder
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public Seeder(RestaurantDbContext dbContext, IPasswordHasher<User> passwordHasher)
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
                _dbContext.Users.RemoveRange(_dbContext.Users.ToList());
                _dbContext.SaveChanges();
                if (!_dbContext.Users.Any())
                {
                    var users = GetUsersWithDetails();


                    _dbContext.Users.AddRange(users);
                    _dbContext.SaveChanges();
                }
            }
        }

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

        private IEnumerable<User> GetUsersWithDetails()
        {
            var date = DateTime.Now;
            var users = new List<User>()
            {
                new User()
                {
                    Email = "test1@test",
                    Password = "pass123",
                    Role = RoleEnum.HeadAdmin,
                    UserDetails = new UserDetails()
                    {
                        CityId = 1,
                        Address = "Testowa 1",
                        Name = "Patryk",
                        Surname = "Zub",
                        PhoneNumber = "123456789",
                        Inserted = date,
                        Updated = date
                    }
                },
                new User()
                {
                    Email = "test2@test",
                    Password = "pass123",
                    Role = RoleEnum.Admin,
                    UserDetails = new UserDetails()
                    {
                        CityId = 1,
                        Address = "Testowa 2",
                        Name = "Patryk 2",
                        Surname = "Zub 2",
                        PhoneNumber = "987654321",
                        Inserted = date,
                        Updated = date
                    }
                },
                new User()
                {
                    Email = "test3@test",
                    Password = "pass123",
                    Role = RoleEnum.Employee,
                    UserDetails = new UserDetails()
                    {
                        CityId = 1,
                        Address = "Testowa 3",
                        Name = "Patryk 3",
                        Surname = "Zub 3",
                        PhoneNumber = "987654321",
                        Inserted = date,
                        Updated = date
                    }
                },
                new User()
                {
                    Email = "test4@test",
                    Password = "pass123",
                    Role = RoleEnum.User,
                    UserDetails = new UserDetails()
                    {
                        CityId = 1,
                        Address = "Testowa 4",
                        Name = "Patryk 4",
                        Surname = "Zub 4",
                        PhoneNumber = "987654321",
                        Inserted = date,
                        Updated = date
                    }
                },
                new User()
                {
                    Email = "test4@test",
                    Password = "pass123",
                    Role = RoleEnum.User,
                    UserDetails = new UserDetails()
                    {
                        CityId = 1,
                        Address = "Testowa 4",
                        Name = "Patryk 4",
                        Surname = "Zub 4",
                        PhoneNumber = "987654321",
                        Inserted = date,
                        Updated = date
                    }
                },
                new User()
                {
                    Email = "test5@test",
                    Password = "pass123",
                    Role = RoleEnum.User,
                    UserDetails = new UserDetails()
                    {
                        CityId = 1,
                        Address = "Testowa 5",
                        PhoneNumber = "987654321",
                        Inserted = date,
                        Updated = date
                    }
                }
            };

            foreach (var user in users)
            {
                user.Password = _passwordHasher.HashPassword(user, user.Password);
            }

            return users;
        }
    }
}
