using Restaurant.DB.Entities;

namespace Restaurant.Business.IRepositories
{
    public interface ICityRepository
    {
        City GetCity(short id);
        City GetCity(string cityName);
        IEnumerable<City> GetCities(bool? cityActivity = null, int pageIndex = 0, int pageSize = 0);
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
