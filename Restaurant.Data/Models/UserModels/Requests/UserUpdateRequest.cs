using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Models.UserModels.Requests
{
    public class UserUpdateRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public short? CityId { get; set; }
        public string Address { get; set; }
    }
}
