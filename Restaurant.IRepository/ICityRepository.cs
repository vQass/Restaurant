using Restaurant.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.IRepository
{
    public interface ICityRepository
    {
        #region GetMethods

        City GetCity(short id);
        City GetCity(string cityName);
        IEnumerable<City> GetCities();

        #endregion

        #region CityModificationMethods

        short AddCity(City city);
        void UpdateCity(City city, string newCityName);
        void DeleteCity(City city);
        void DisableCity(City city);
        void EnableCity(City city);

        #endregion

        #region ValidationMethods

        void EnsureCityExists(City city);
        void EnsureCityNameNotTaken(string cityName, short id = 0);
        void EnsureCityNotInUse(City city);
        void EnsureCityNotUsedInUsers(City city);
        void EnsureCityNotUsedInOrders(City city);

        #endregion
    }
}
