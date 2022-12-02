using Restaurant.Business.IRepositories;
using Restaurant.Business.IServices;
using Restaurant.Data.Models.CityModels;
using Restaurant.Entities.Entities;

namespace Restaurant.Business.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(
            ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }


        public City GetCity(short id)
        {
            var city = _cityRepository.GetCity(id);

            _cityRepository.EnsureCityExists(city);

            return city;
        }

        public CityWrapper GetCities(bool? cityActivity, int pageIndex = 0, int pageSize = 0)
        {
            var cities = _cityRepository.GetCities(cityActivity, pageIndex, pageSize);
            var cityCount = _cityRepository.GetCitiesCount();

            return new CityWrapper()
            {
                Items = cities,
                ItemCount = cityCount
            };
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
    }
}
