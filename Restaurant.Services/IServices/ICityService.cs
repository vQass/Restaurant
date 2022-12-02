using Restaurant.Data.Models.CityModels;
using Restaurant.DB.Entities;

namespace Restaurant.Business.IServices
{
    public interface ICityService
    {
        public CityWrapper GetCities(bool? cityActivity, int pageIndex = 0, int pageSize = 0);
        public City GetCity(short id);
        public short AddCity(string cityName);
        public void UpdateCity(short id, string cityName);
        public void DeleteCity(short id);
        public void EnableCity(short id);
        public void DisableCity(short id);
    }
}
