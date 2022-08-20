using Restaurant.DB.Entities;
using Restaurant.IRepository;
using Restaurant.IServices;

namespace Restaurant.Services.Services
{
    public class CityService : ICityService
    {
        #region Fields

        private readonly ICityRepository _cityRepository;

        #endregion

        #region Ctors

        public CityService(
            ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        #endregion

        #region PublicMethods

        public City GetCity(short id)
        {
            var city = _cityRepository.GetCity(id);

            _cityRepository.EnsureCityExists(city);

            return city;
        }

        public IEnumerable<City> GetCities()
        {
            return _cityRepository.GetCities();
        }

        public short AddCity(string cityName)
        {
            _cityRepository.EnsureCityNameNotTaken(cityName);

            var cityId = _cityRepository
                .AddCity(new City() { Name = cityName.Trim() }); 

            return cityId;
        }

        public void DeleteCity(short id)
        {
            var city = _cityRepository.GetCity(id);

            _cityRepository.EnsureCityExists(city);

            _cityRepository.EnsureCityNotInUse(city);

            _cityRepository.DeleteCity(city);
        }

        public void DisableCity(short id)
        {
            var city = _cityRepository.GetCity(id);

            _cityRepository.EnsureCityExists(city);

            _cityRepository.DisableCity(city);
        }

        public void EnableCity(short id)
        {
            var city = _cityRepository.GetCity(id);

            _cityRepository.EnsureCityExists(city);

            _cityRepository.EnableCity(city);
        }

        public void UpdateCity(short id, string cityName)
        {
            var city = _cityRepository.GetCity(id);

            _cityRepository.EnsureCityExists(city);

            _cityRepository.EnsureCityNameNotTaken(cityName, id);

            _cityRepository.EnsureCityNotInUse(city);

            _cityRepository.UpdateCity(city, cityName.Trim());
        }

        #endregion
    }
}
