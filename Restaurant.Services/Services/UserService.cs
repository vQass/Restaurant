using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Authentication;
using Restaurant.Business.IRepositories;
using Restaurant.Business.IServices;
using Restaurant.Data.Exceptions;
using Restaurant.Data.Models.UserModels.Requests;
using Restaurant.Data.Models.UserModels.Responses;
using Restaurant.Data.Models.UserModels.ViewModels;
using Restaurant.Entities.Entities;
using Restaurant.Entities.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurant.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly AuthenticationSettings _authenticationSettings;

        public UserService(
            IPasswordHasher<User> passwordHasher,
            AuthenticationSettings authenticationSettings,
            IUserRepository userRepository)
        {
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _userRepository = userRepository;
        }

        public long AddUser(UserCreateRequest userCreateRequest)
        {
            _userRepository.EnsureEmailHasValidFormat(userCreateRequest.Email);

            _userRepository.EnsureEmailNotTaken(userCreateRequest.Email);

            var id = _userRepository.AddUser(userCreateRequest);

            return id;
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
    }
}
