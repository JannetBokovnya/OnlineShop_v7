using Microsoft.AspNetCore.Identity;
using OnlineShop.Library.Common.Models;

namespace OnlineShop.Library.UserManagementService.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Address? DefaultAddress { get; set; }

        public Address? DeliveryAddress { get; set; }
    }
}
