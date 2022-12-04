using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.Business.IRepositories;
using Restaurant.Data.Exceptions;
using Restaurant.Data.Models.UserModels.Requests;
using Restaurant.DB;
using Restaurant.Entities.Entities;
using Restaurant.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly ILogger<UserRepository> _logger;
        private IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserRepository(
            RestaurantDbContext dbContext,
            ILogger<UserRepository> logger,
            IPasswordHasher<User> passwordHasher,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        #region GetMethods

        public async Task<IEnumerable<User>> GetUsers(List<long> ids)
        {
            return await _dbContext.Users.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public User GetUser(string email)
        {
            return _dbContext.Users
                .FirstOrDefault(x => x.Email
                    .ToLower()
                    .Equals(email
                        .ToLower()));
        }

        #endregion

        #region EntityModificationMethods

        public long AddUser(UserCreateRequest userCreateRequest)
        {
            var date = DateTime.Now;
            var user = _mapper.Map<User>(userCreateRequest);

            user.Password = _passwordHasher.HashPassword(user, user.Password);
            user.Inserted = date;
            user.Updated = date;
            user.Role = (byte)RoleEnum.User;

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user.Id;
        }

        #endregion

        #region ValidationMethods

        public void EnsureUserExists(User user)
        {
            if (user == null)
            {
                throw new NotFoundException("Podany użytkownik nie istnieje.");
            }
        }

        public void EnsureEmailNotTaken(string email, long id = 0)
        {
            var emailInUse = _dbContext.Users
                .Where(x => x.Id != id)
                .Any(x => x.Email
                    .ToLower()
                    .Equals(email
                        .ToLower()));

            if (emailInUse)
            {
                _logger.LogError($"Email:{email} is already taken.");
                throw new BadRequestException("Podany email jest zajęty.");
            }
        }

        public void EnsureEmailHasValidFormat(string email)
        {
            var valiadtor = new EmailAddressAttribute();

            var emailValid = valiadtor.IsValid(email);

            if (!emailValid)
            {
                _logger.LogError($"Email:{email} has invalid format.");
                throw new BadRequestException("Podany email ma błędny format.");
            }
        }

        #endregion
    }
}
