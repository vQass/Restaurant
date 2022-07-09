using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RestaurantAPI.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurant.API.Services
{
    //public class AccountService : IAccountService
    //{
    //    readonly RestaurantDbContext _dbContext;
    //    readonly IMapper _mapper;
    //    readonly IPasswordHasher<Administrator> _passwordHasher;
    //    readonly AuthenticationSettings _authenticationSettings;
    //    public AccountService(WikipediaDbContext dbContext, IMapper mapper, IPasswordHasher<Administrator> passwordHasher, AuthenticationSettings authenticationSettings)
    //    {
    //        _dbContext = dbContext;
    //        _mapper = mapper;
    //        _passwordHasher = passwordHasher;
    //        _authenticationSettings = authenticationSettings;
    //    }

    //    public void Add(AdministratorCreateDto adminDto)
    //    {
    //        var admin = _mapper.Map<Administrator>(adminDto);

    //        admin.Password = _passwordHasher.HashPassword(admin, admin.Password);

    //        _dbContext.Administrators.Add(admin);
    //        _dbContext.SaveChanges();
    //    }

    //    public string GenerateJwt(LoginDto login)
    //    {
    //        var admin = _dbContext.Administrators.FirstOrDefault(x => x.Email == login.Email);

    //        if (admin is null)
    //        {
    //            throw new BadRequestException("Invalid email or password");
    //        }

    //        var result = _passwordHasher.VerifyHashedPassword(admin, admin.Password, login.Password);

    //        if (result == PasswordVerificationResult.Failed)
    //        {
    //            throw new BadRequestException("Invalid email or password");
    //        }

    //        var claims = new List<Claim>
    //        {
    //            new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
    //            new Claim(ClaimTypes.Email, admin.Email),
    //        };

    //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
    //        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //        var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
    //        var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims, expires: expires, signingCredentials: cred);

    //        var tokenHandler = new JwtSecurityTokenHandler();
    //        return tokenHandler.WriteToken(token);
    //    }

    //    public void Delete(int id)
    //    {
    //        //Not the best solution, admin is able to remove himself

    //        var admin = _dbContext.Administrators.FirstOrDefault(x => x.Id == id);

    //        if (admin is null)
    //        {
    //            throw new NotFoundException("Administrator with given id does not exist");
    //        }

    //        _dbContext.Administrators.Remove(admin);
    //        _dbContext.SaveChanges();
    //    }

    //    public object GetAdmins()
    //    {
    //        var admins = _dbContext.Administrators.Select(x => new { x.Id, x.Email }).ToList();

    //        return admins;
    //    }
    //}
}
