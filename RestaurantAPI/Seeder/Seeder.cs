using Microsoft.AspNetCore.Identity;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Seeder
{
    public class Seeder
    {
        //private readonly RestaurantDbContext _dbContext;
        //private readonly IPasswordHasher<User> _passwordHasher;

        //public Seeder(RestaurantDbContext dbContext, IPasswordHasher<User> passwordHasher)
        //{
        //    _dbContext = dbContext;
        //    _passwordHasher = passwordHasher;
        //}

        //public void Seed()
        //{
        //    if (_dbContext.Database.CanConnect())
        //    {
        //        if (!_dbContext.Tabela.Any())
        //        {
        //            var articles = GetArticles();
        //            _dbContext.Articles.AddRange(articles);
        //            _dbContext.SaveChanges();
        //        }
                
        //    }
        //}

        //private IEnumerable<Administrator> GetAdmins()
        //{
        //    var admin1 = new Administrator()
        //    {
        //        Email = "test@test",
        //    };
        //    admin1.Password = _passwordHasher.HashPassword(admin1, "pass123");

        //    var admins = new List<Administrator>()
        //    {
        //        admin1
        //    };
        //    return admins;
        //}
    }
}
