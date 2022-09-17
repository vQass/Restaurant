using Restaurant.DB.Entities;
using Restaurant.DB.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Models.UserModels.ViewModels
{
    public class UserWithDetailsViewModel
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public bool IsActive { get; set; } = true;

        public string Name { get; set; }

        public string Surname { get; set; }

        public City City { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime Inserted { get; set; }

        public DateTime Updated { get; set; }
    }
}
