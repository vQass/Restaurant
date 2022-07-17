using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Restaurant.APIComponents;
using Restaurant.APIComponents.Exceptions;
using Restaurant.Data.Models.UserModels;
using Restaurant.DB;
using Restaurant.DB.Entities;
using Restaurant.DB.Enums;
using Restaurant.IServices.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurant.Services.Services
{
    public class UserService : IUserService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly ILogger<UserService> _logger;

        public UserService(
            RestaurantDbContext dbContext,
            IMapper mapper,
            IPasswordHasher<User> passwordHasher,
            AuthenticationSettings authenticationSettings,
            ILogger<UserService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _logger = logger;
        }

        public long AddUser(UserCreateRequestDto dto)
        {
            var date = new DateTime();

            var user = _mapper.Map<User>(dto);
            var userDetails = _mapper.Map<UserDetails>(dto);

            user.Password = _passwordHasher.HashPassword(user, user.Password);

            userDetails.Inserted = date;
            userDetails.Updated = date;

            user.UserDetails = userDetails;
            user.Role = (byte)RoleEnum.User;

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user.Id;
        }

        public void DisableUser(long id, List<Claim> userClaims)
        {
            var users = _dbContext.Users.ToList();

            var user = _dbContext.Users.FirstOrDefault(x => x.Id == id);

            if (user is null)
            {
                throw new NotFoundException("Użytkownik z podanym id nie istnieje.");
            }

            var currentUserIdFromClaim = userClaims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value;

            long currentUserId;

            if(!long.TryParse(currentUserIdFromClaim, out currentUserId))
            {
                _logger.LogError("Error during convering current user id from claims");
                throw new InternalErrorException("Błąd podczas konwertowania twojego ID na wartość numeryczną, skontaktuj się z oddziałem wsparcia.");
            }

            if (user.Id == currentUserId)
            {
                throw new BadRequestException("Nie można oznaczyć swojego konta jako nieaktywne.");
            }

            var role = userClaims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role, StringComparison.OrdinalIgnoreCase))?.Value;

            if(role.Equals(RoleEnum.Admin.ToString()) && (
                user.Role.Equals(RoleEnum.Admin.ToString()) ||
                user.Role.Equals(RoleEnum.HeadAdmin.ToString())))
            {
                throw new BadRequestException("Administrator nie możne oznaczyć konta innego administratora jako nieaktywne.");
            }

            user.IsActive = false;
            _dbContext.SaveChanges();
        }

        public List<UserListItemDto> GetUsersList()
        {
            var users = _dbContext.Users.ToList();

            var usersAsListItem = _mapper.Map<List<UserListItemDto>>(users);

            return usersAsListItem;
        }

        public string SignInUser(LoginDto dto)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == dto.Email);

            if (user is null)
            {
                throw new BadRequestException("Invalid email or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid email or password");
            }

            return GenerateJwt(user);
        }

        internal string GenerateJwt(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims, expires: expires, signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
