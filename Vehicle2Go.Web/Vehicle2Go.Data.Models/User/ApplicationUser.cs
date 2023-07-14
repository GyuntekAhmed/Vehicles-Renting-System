using Vehicle2Go.Data.Models.Vehicle;

namespace Vehicle2Go.Data.Models.User
{
    using Microsoft.AspNetCore.Identity;
    using System;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.RentedCars = new HashSet<Car>();
        }

        public ICollection<Car> RentedCars { get; set; }
    }
}
