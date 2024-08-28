using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Library.UserManagementService.Requests
{
    public class UserPasswordChangeRequest
    {
        public string? UserName { get; set; }
        public string? CurrentPassword { get; set; }

        public string? NewPassword { get; set; }
    }
}
