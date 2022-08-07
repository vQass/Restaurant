using Restaurant.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.IServices
{
    public interface ICityService
    {
        public IEnumerable<City> GetCityList();
        public City GetCityById(short id);
        public short AddCity(string cityName);
        public void UpdateCity(short id, string cityName);
        public void DeleteCity(short id);
        public void EnableCity(short id);
        public void DisableCity(short id);
    }
}
