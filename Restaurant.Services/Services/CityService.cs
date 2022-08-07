using Microsoft.Extensions.Logging;
using Restaurant.APIComponents.Exceptions;
using Restaurant.DB;
using Restaurant.DB.Entities;
using Restaurant.IServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Services
{
    public class CityService : ICityService
    {
        #region Fields

        private readonly RestaurantDbContext _dbContext;
        private readonly ILogger _logger;

        #endregion

        #region Ctors

        public CityService(RestaurantDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #endregion

        #region PublicMethods

        public void AddCity(string cityName)
        {
            CheckIfCityNameInUse(cityName);

            var city = new City();
            city.Name = cityName;

            _dbContext.Cities.Add(city);
            _dbContext.SaveChanges();
        }

        public void DeleteCity(short id)
        {
            var city = CheckIfCityExistsById(id);

            CheckIfCityInUse(city);

            _dbContext.Cities.Remove(city);
            _dbContext.SaveChanges();
        }

        public void DisableCity(short id)
        {
            var city = CheckIfCityExistsById(id);

            city.IsActive = false;
            _dbContext.SaveChanges();
        }

        public void EnableCity(short id)
        {
            var city = CheckIfCityExistsById(id);

            city.IsActive = true;
            _dbContext.SaveChanges();
        }

        public City GetCityById(short id)
        {
            var city = CheckIfCityExistsById(id);

            return city;
        }

        public IEnumerable<City> GetCityList()
        {
            var cities = _dbContext.Cities.ToList();
            
            return cities;
        }

        public void UpdateCity(short id, string cityName)
        {
            var city = CheckIfCityExistsById(id);

            CheckIfCityNameInUse(cityName, id);

            city.Name = cityName;
            _dbContext.SaveChanges();
        }

        #endregion

        #region PrivateMethods

        private City CheckIfCityExistsById(short id)
        {
            var city = _dbContext.Cities.FirstOrDefault(x => x.Id == id);

            if(city is null)
            {
                throw new NotFoundException("Miasto o podanym id nie istnieje.");
            }

            return city;
        }

        private City CheckIfCityExistsByName(string cityName)
        {
            var city = _dbContext.Cities
                .FirstOrDefault(x => x.Name == cityName);

            if (city is null)
            {
                throw new NotFoundException("Miasto o podanej nazwie nie istnieje.");
            }

            return city;
        }

        private void CheckIfCityNameInUse(string cityName, short id = 0)
        {
            var cityNameInUse = _dbContext.Cities
                .Where(x => x.Id != id)
                .Any(
                    x => x.Name.Trim().ToLower() ==
                    cityName.Trim().ToLower());

            if (cityNameInUse)
            {
                _logger.LogError("Given name is taken.");
                throw new NotFoundException("Podana nazwa miasta jest zajęta.");
            }
        }

        private void CheckIfCityInUse(City city)
        {
            var cityUsedInUsers = _dbContext.Users.Any(x => x.City == city);

            if(cityUsedInUsers)
            {
                _logger.LogError($"City with name {city.Name} is used in users table.");
                throw new BadRequestException("Podane miasto używane jest w adresie co najmniej jednego użytkownika.");
            }

            var cityUsedInOrders = _dbContext.Orders.Any(x => x.City == city);

            if (cityUsedInUsers)
            {
                _logger.LogError($"City with name {city.Name} is used in orders table.");
                throw new BadRequestException("Podane miasto używane jest w adresie co najmniej jednego zamówienia.");
            }
        }

        #endregion
    }
}
