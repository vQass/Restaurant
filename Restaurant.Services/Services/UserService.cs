using AutoMapper;
using Restaurant.Data.Models.UserModels;
using Restaurant.DB;
using Restaurant.DB.Entities;
using Restaurant.IServices.Interfaces;

namespace Restaurant.Services.Services
{
    public class UserService : IUserService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public long AddUser(UserCreateRequestDto dto)
        {
            var date = new DateTime();

            var user = _mapper.Map<User>(dto);
            var userDetails = _mapper.Map<UserDetails>(dto);

            userDetails.Inserted = date;
            userDetails.Updated = date;

            user.UserDetails = userDetails;

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user.Id;
        }
    }
}
