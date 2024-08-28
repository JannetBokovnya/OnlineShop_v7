using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Library.UserManagementService.Requests
{
    public class AddRemoveRoleRequest
    {
        public string UserName { get; set; }

        public string RoleName { get; set; }
    }
}
