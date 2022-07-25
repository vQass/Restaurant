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
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Services.Services
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly ILogger<UserService> _logger;

        #endregion Fields

        #region Ctors

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

        #endregion Ctors

        #region PublicMethods

        public long AddUser(UserCreateRequest userCreateRequest)
        {
            var date = new DateTime();

            var user = _mapper.Map<User>(userCreateRequest);
            var userDetails = _mapper.Map<UserDetails>(userCreateRequest);

            user.Password = _passwordHasher.HashPassword(user, user.Password);

            userDetails.Inserted = date;
            userDetails.Updated = date;

            user.UserDetails = userDetails;
            user.Role = (byte)RoleEnum.User;

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user.Id;
        }

        public void UpdateUser(long id, UserUpdateRequest userUpdateRequest)
        {
            var user = CheckIfUserExistsById(id);

            _dbContext.Entry(user).Reference(x => x.UserDetails).Load();

            user.UserDetails.Name = userUpdateRequest.Name;
            user.UserDetails.Surname = userUpdateRequest.Surname;
            user.UserDetails.Address = userUpdateRequest.Address;
            user.UserDetails.CityId = userUpdateRequest.CityId;
            user.UserDetails.PhoneNumber = userUpdateRequest.PhoneNumber;
            user.UserDetails.Updated = new DateTime();

            _dbContext.SaveChanges();
        }

        public void UpdateUserEmail(long id, string newEmail)
        {
            var user = CheckIfUserExistsById(id);

            CheckIfEmailHasValidFormat(newEmail);

            CheckIfEmailInUse(newEmail, id);

            user.Email = newEmail;

            _dbContext.SaveChanges();
        }

        public void DisableUser(long id, List<Claim> userClaims)
        {
            var user = CheckIfUserExistsById(id);

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
                throw new UnauthorizedException("Administrator nie możne oznaczyć konta innego administratora jako nieaktywne.");
            }

            user.IsActive = false;
            _dbContext.SaveChanges();
        }

        public List<UserListViewModel> GetUsersList()
        {
            var users = _dbContext.Users.ToList();

            var usersAsListItem = _mapper.Map<List<UserListViewModel>>(users);

            return usersAsListItem;
        }

        public string SignInUser(LoginRequest loginRequest)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == loginRequest.Email);

            CheckIfUserExistsByEmial(loginRequest.Email, "Niepoprawny email lub hasło.");

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginRequest.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Niepoprawny email lub hasło.");
            }

            return GenerateJwt(user);
        }

        #endregion PublicMethods

        #region PrivateMethods

        private string GenerateJwt(User user)
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

        private User CheckIfUserExistsById(long id)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                _logger.LogError($"User with id:{id} does not exist.");
                throw new NotFoundException("Użytkownik o podanym id nie istnieje.");
            }

            return user;
        }

        private User CheckIfUserExistsByEmial(string email, string message)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                _logger.LogError($"User with email:{email} does not exist.");
                throw new NotFoundException(message);
            }

            return user;
        }

        private void CheckIfEmailInUse(string email, long id = 0)
        {
            var emailInUse = _dbContext.Users.Where(x => x.Id != id).Any(x => x.Email.ToLower() == email.ToLower());

            if(emailInUse)
            {
                throw new BadRequestException("Podany email jest zajęty.");
            }
        }

        private void CheckIfEmailHasValidFormat(string email)
        {
            var valiadtor = new EmailAddressAttribute();

            var emailValid = valiadtor.IsValid(email);

            if(!emailValid)
            {
                throw new BadRequestException("Podany email ma błędny format.");
            }
        }

        #endregion PrivateMethods
    }
}
