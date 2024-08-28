using OnlineShop.Library.UserManagementService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Library.UserManagementService.Requests
{
    public class CreateUserRequest
    {
        public ApplicationUser? User { get; set; }
        public string? Password { get; set; }
    }
}
