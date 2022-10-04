using Restaurant.DB.Entities;

namespace Restaurant.IRepository
{
    public interface ICityRepository
    {
        IQueryable<City> GetCity(short id);
        IQueryable<City> GetCity(string cityName);
        IQueryable<City> GetCities();
        short GetCitiesCount();

        short AddCity(City city);
        void UpdateCity(City city, string newCityName);
        void DeleteCity(City city);
        void DisableCity(City city);
        void EnableCity(City city);

        void EnsureCityExists(City city);
        void EnsureCityNameNotTaken(string cityName, short id = 0);
        void EnsureCityNotInUse(City city);
        void EnsureCityNotUsedInUsers(City city);
        void EnsureCityNotUsedInOrders(City city);
    }
}
