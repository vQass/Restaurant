using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.APIComponents.Exceptions;
using Restaurant.DB;
using Restaurant.DB.Entities;
using Restaurant.IRepository;

namespace Restaurant.Repository.Repositories
{
    public class CityRepository : ICityRepository
    {
        #region Fields

        private readonly RestaurantDbContext _dbContext;
        private readonly ILogger<CityRepository> _logger;

        #endregion

        #region Ctors

        public CityRepository(RestaurantDbContext dbContext,
            ILogger<CityRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #endregion

        #region Methods

        #region GetMethods

        public City GetCity(short id)
        {
            return _dbContext.Cities.FirstOrDefault(x => x.Id == id);
        }

        public City GetCity(string cityName)
        {
            return _dbContext.Cities.FirstOrDefault(x => x.Name == cityName);
        }

        public IEnumerable<City> GetCities()
        {
            return _dbContext.Cities
                .AsNoTracking()
                .ToList();
        }

        #endregion

        #region EntityModificationMethods

        public short AddCity(City city)
        {
            _dbContext.Cities.Add(city);
            _dbContext.SaveChanges();
            return city.Id;
        }

        public void UpdateCity(City city, string newCityName)
        {
            city.Name = newCityName;
            _dbContext.SaveChanges();
        }

        public void DeleteCity(City city)
        {
            _dbContext.Cities.Remove(city);
            _dbContext.SaveChanges();
        }

        public void DisableCity(City city)
        {
            city.IsActive = false;
            _dbContext.SaveChanges();
        }

        public void EnableCity(City city)
        {
            city.IsActive = true;
            _dbContext.SaveChanges();
        }

        #endregion

        #region ValidationMethods

        public void EnsureCityExists(City city)
        {
            if (city is null)
            {
                throw new NotFoundException("Podane miasto nie istnieje.");
            }
        }

        public void EnsureCityNameNotTaken(string cityName, short id = 0)
        {
            var cityNameInUse = _dbContext.Cities
                .Where(x => x.Id != id)
                .Any(x => x.Name.Equals(cityName.Trim(), StringComparison.InvariantCultureIgnoreCase));

            if (cityNameInUse)
            {
                _logger.LogError($"Name: {cityName} is taken.");
                throw new BadRequestException("Podana nazwa miasta jest zajęta.");
            }
        }

        public void EnsureCityNotInUse(City city)
        {
            EnsureCityNotUsedInUsers(city);
            EnsureCityNotUsedInOrders(city);
        }

        public void EnsureCityNotUsedInUsers(City city)
        {
            var cityUsedInUsers = _dbContext.Users.Any(x => x.City == city);

            if (cityUsedInUsers)
            {
                _logger.LogError($"City with name {city.Name} is used in users table.");
                throw new BadRequestException("Podane miasto używane jest w adresie co najmniej jednego użytkownika.");
            }
        }

        public void EnsureCityNotUsedInOrders(City city)
        {
            var cityUsedInOrders = _dbContext.Orders.Any(x => x.City == city);

            if (cityUsedInOrders)
            {
                _logger.LogError($"City with name {city.Name} is used in orders table.");
                throw new BadRequestException("Podane miasto używane jest w adresie co najmniej jednego zamówienia.");
            }
        }

        #endregion

        #endregion
    }
}
