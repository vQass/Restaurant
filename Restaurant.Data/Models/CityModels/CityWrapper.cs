using Restaurant.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Models.CityModels
{
    public class CityWrapper
    {
        public IEnumerable<City> Items { get; set; }
        public short ItemCount  { get; set; }
    }
}
