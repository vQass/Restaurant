using Restaurant.Data.Models.CityModels;
using Restaurant.DB.Entities;

namespace Restaurant.IServices
{
    public interface ICityService
    {
        public CityWrapper GetCities(bool? cityActivity);
        public City GetCity(short id);
        public short AddCity(string cityName);
        public void UpdateCity(short id, string cityName);
        public void DeleteCity(short id);
        public void EnableCity(short id);
        public void DisableCity(short id);
    }
}
