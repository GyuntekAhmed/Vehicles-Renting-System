namespace Vehicle2Go.Data.Models.User
{
    using Microsoft.AspNetCore.Identity;
    using System;

    using Vehicle;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.RentedCars = new HashSet<Car>();
            this.RentedMotorcycles = new HashSet<Motorcycle>();
        }

        public ICollection<Car> RentedCars { get; set; }

        public ICollection<Motorcycle> RentedMotorcycles { get; set; }
    }
}
