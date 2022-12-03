using Restaurant.Data.Models.CityModels;

namespace Restaurant.Business.IServices
{
    public interface ICityService
    {
        CityViewModel GetCity(short id);
        List<CityViewModel> GetCities(bool? cityActivity);
        CityWrapper GetCityPage(int pageIndex = 0, int pageSize = 0);

        void AddCity(CityCreateRequest city);
        void UpdateCity(short id, CityUpdateRequest city);
        void DeleteCity(short id);
        
        void EnableCity(short id);
        void DisableCity(short id);
    }
}
