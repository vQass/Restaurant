using Restaurant.Data.Models.UserModels;
using Restaurant.DB;
using Restaurant.Services.Interfaces;

namespace Restaurant.Services.Services
{
    public class UserService : IUserService
    {
        private readonly RestaurantDbContext _dbContext;

        public UserService(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUser(UserCreateRequestDto dto)
        {

        }
    }
}
