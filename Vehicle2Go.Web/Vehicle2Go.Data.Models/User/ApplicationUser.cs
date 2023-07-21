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
            this.RentedJets = new HashSet<Jet>();
            this.RentedYachts = new HashSet<Yacht>();
            this.RentedTrucks = new HashSet<Truck>();
        }

        public ICollection<Car> RentedCars { get; set; }

        public ICollection<Motorcycle> RentedMotorcycles { get; set; }

        public ICollection<Jet> RentedJets { get; set; }

        public ICollection<Yacht> RentedYachts { get; set; }

        public ICollection<Truck> RentedTrucks { get; set; }
    }
}
