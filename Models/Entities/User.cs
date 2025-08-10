using Microsoft.AspNetCore.Identity;

namespace WebPharmecyDiscountdemo.Models.Entities
{
    public class User : IdentityUser // string Id comes from IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
