using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Restaurant.APIComponents;
using Restaurant.APIComponents.Exceptions;
using Restaurant.Business.IServices;
using Restaurant.Data.Models.UserModels.Requests;
using Restaurant.Data.Models.UserModels.Responses;
using Restaurant.Data.Models.UserModels.ViewModels;
using Restaurant.DB.Entities;
using Restaurant.DB.Enums;
using Restaurant.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurant.Business.Services
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        #endregion Fields

        #region Ctors

        public UserService(
            IMapper mapper,
            IPasswordHasher<User> passwordHasher,
            AuthenticationSettings authenticationSettings,
            IUserRepository userRepository,
            ILogger<UserService> logger)
        {
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _userRepository = userRepository;
            _logger = logger;
        }

        #endregion Ctors

        #region PublicMethods

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            var users = await _userRepository.GetUsers();

            var usersViewModels = _mapper.Map<List<UserListViewModel>>(users);

            return usersViewModels;
        }

        public UserWithDetailsViewModel GetUser(long id)
        {
            var user = _userRepository.GetUser(id);

            _userRepository.EnsureUserExists(user);

            var userWithDetails = _mapper.Map<UserWithDetailsViewModel>(user);

            return userWithDetails;
        }

        public long AddUser(UserCreateRequest userCreateRequest)
        {
            _userRepository.EnsureEmailHasValidFormat(userCreateRequest.Email);

            _userRepository.EnsureEmailNotTaken(userCreateRequest.Email);

            var id = _userRepository.AddUser(userCreateRequest);

            return id;
        }

        public void UpdateUser(long id, UserUpdateRequest userUpdateRequest)
        {
            var user = _userRepository.GetUser(id);

            _userRepository.EnsureUserExists(user);

            _userRepository.UpdateUser(user, userUpdateRequest);
        }

        public void UpdateUserEmail(long id, string newEmail)
        {
            var user = _userRepository.GetUser(id);

            _userRepository.EnsureUserExists(user);

            _userRepository.EnsureEmailHasValidFormat(newEmail);

            _userRepository.EnsureEmailNotTaken(newEmail, id);

            _userRepository.UpdateUserEmail(user, newEmail);
        }

        public void DisableUser(long id, List<Claim> userClaims)
        {
            var user = _userRepository.GetUser(id);

            _userRepository.EnsureUserExists(user);

            var loggedInUsersIdFromClaims = GetIdFromClaims(userClaims);

            long adminId = TryParseLoggedInUsersId(loggedInUsersIdFromClaims);

            EnsureUserNotDisablingOwnAccount(user.Id, adminId);

            var role = GetRoleFromClaims(userClaims);

            EnsureUserHasPermissionToDisableAccount(user, role);

            _userRepository.DisableUser(user);
        }

        public LoginResponse SignInUser(LoginRequest loginRequest)
        {
            var user = _userRepository.GetUser(loginRequest.Email);

            EnsureUserExistsForSigningIn(user);

            var passwordVerificationResult = ComparePasswordHashes(user, loginRequest);

            EnsurePasswordVerificationNotFailed(passwordVerificationResult);

            var token = GenerateJwt(user);

            var response = new LoginResponse()
            {
                JwtToken = token,
                Role = user.Role.ToString(),
                Id = user.Id
            };

            return response;
        }

        #endregion PublicMethods

        #region PrivateMethods

        #region JwtToken

        private string GenerateJwt(User user)
        {
            var claims = GetUserClaims(user);

            var securityKey = GetSymmetricSecurityKey(_authenticationSettings.JwtKey);
            var signingCredentials = GetSigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var keyExpirationDateTime = GetJwtExpirationDateTime(_authenticationSettings.JwtExpireDays);

            var token = GetJwtToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                keyExpirationDateTime,
                signingCredentials);

            return SerializeToken(token);
        }

        private List<Claim> GetUserClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
        }

        private SymmetricSecurityKey GetSymmetricSecurityKey(string jwtKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        }

        private SigningCredentials GetSigningCredentials(SymmetricSecurityKey key, string securityAlgorithms)
        {
            return new SigningCredentials(key, securityAlgorithms);
        }

        private DateTime GetJwtExpirationDateTime(int timeInDays)
        {
            return DateTime.Now.AddDays(timeInDays);
        }

        private JwtSecurityToken GetJwtToken(
            string issuer,
            string audience,
            List<Claim> claims,
            DateTime expirationDateTime,
            SigningCredentials signingCredentials)
        {
            return new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expirationDateTime,
                signingCredentials: signingCredentials);
        }

        private string SerializeToken(JwtSecurityToken token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        #endregion

        #region ClaimsParsing

        private long TryParseLoggedInUsersId(string id)
        {
            long parsedId;

            if (!long.TryParse(id, out parsedId))
            {
                _logger.LogError("Error during convering current user id from claims");
                throw new InternalErrorException("Błąd podczas konwertowania twojego ID na wartość numeryczną, skontaktuj się z oddziałem wsparcia.");
            }

            return parsedId;
        }

        private string GetIdFromClaims(List<Claim> userClaims)
        {
            return userClaims?
                .FirstOrDefault(x => x.Type
                    .Equals(
                        ClaimTypes.NameIdentifier,
                        StringComparison.OrdinalIgnoreCase))
                ?.Value;
        }

        private string GetRoleFromClaims(List<Claim> userClaims)
        {
            return userClaims?
                .FirstOrDefault(x => x.Type
                    .Equals(
                        ClaimTypes.Role,
                        StringComparison.OrdinalIgnoreCase))
                ?.Value;
        }

        #endregion

        #region AccountDisablingValidation

        private void EnsureUserNotDisablingOwnAccount(long userId, long adminId)
        {
            if (userId == adminId)
            {
                throw new BadRequestException("Nie można oznaczyć swojego konta jako nieaktywne.");
            }
        }

        private void EnsureUserHasPermissionToDisableAccount(User user, string role)
        {
            if (role.Equals(RoleEnum.Admin.ToString())
                    && (user.Role.Equals(RoleEnum.Admin.ToString())
                || user.Role.Equals(RoleEnum.HeadAdmin.ToString())))
            {
                throw new UnauthorizedException("Administrator nie możne oznaczyć konta innego administratora jako nieaktywne.");
            }
        }

        #endregion

        #region PasswordHasher

        private PasswordVerificationResult ComparePasswordHashes(User user, LoginRequest loginRequest)
        {
            return _passwordHasher.VerifyHashedPassword(user, user.Password, loginRequest.Password);
        }

        private void EnsurePasswordVerificationNotFailed(PasswordVerificationResult passwordVerificationResult)
        {
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Niepoprawny email lub hasło.");
            }
        }

        #endregion

        #region Validation

        private void EnsureUserExistsForSigningIn(User user)
        {
            try
            {
                _userRepository.EnsureUserExists(user);
            }
            catch (NotFoundException)
            {
                throw new BadRequestException("Niepoprawny email lub hasło.");
            }
        }

        #endregion

        #endregion
    }
}
