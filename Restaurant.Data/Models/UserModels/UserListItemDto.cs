using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Models.UserModels
{
    public class UserListItemDto
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
