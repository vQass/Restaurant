using AutoMapper;
using Restaurant.Business.IRepositories;
using Restaurant.Business.IServices;
using Restaurant.Data.Models.CityModels;

namespace Restaurant.Business.Services
{
    internal class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityService(
            ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public CityViewModel GetCity(short id)
        {
            var city = _cityRepository.GetCity(id);

            _cityRepository.EnsureCityExists(city);

            var cityVM = _mapper.Map<CityViewModel>(city);

            return cityVM;
        }

        public List<CityViewModel> GetCities(bool? cityActivity)
        {
            var cities = _cityRepository.GetCities(cityActivity);
            var citiesVM = _mapper.Map<List<CityViewModel>>(cities);
            return citiesVM;
        }

        public CityWrapper GetCityPage(int pageIndex, int pageSize)
        {
            var cities = _cityRepository.GetCities(pageIndex: pageIndex, pageSize: pageSize);

            var citiesVM = _mapper.Map<List<CityViewModel>>(cities);
            var cityCount = _cityRepository.GetCitiesCount();

            var wrapper = new CityWrapper
            {
                Items = citiesVM,
                ItemCount = cityCount
            };

            return wrapper;
        }

        public void AddCity(CityCreateRequest city)
        {
            _cityRepository.EnsureCityNameNotTaken(city.Name);
            _cityRepository.AddCity(city);
        }
        public void UpdateCity(short id, CityUpdateRequest cityRequest)
        {
            var city = _cityRepository.GetCity(id);

            _cityRepository.EnsureCityExists(city);

            _cityRepository.EnsureCityNameNotTaken(cityRequest.Name, id);

            _cityRepository.EnsureCityNotInUse(city);

            _cityRepository.UpdateCity(city, cityRequest);
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
    }
}
