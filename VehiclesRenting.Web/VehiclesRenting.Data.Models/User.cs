namespace VehiclesRenting.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    
    // This is custom user class that works with IdentityUser.

    public class User : IdentityUser<Guid>
    {
        public User()
        {
            this.Id = Guid.NewGuid();
            this.RentedCars = new List<Car>();
            this.RentedMotorcycles = new List<Motorcycle>();
            this.RentedScooters = new List<Scooter>();
        }
        
        public string? CurrentAddress { get; set; }

        public virtual ICollection<Car> RentedCars { get; set; }

        public virtual ICollection<Motorcycle> RentedMotorcycles { get; set; }

        public virtual ICollection<Scooter> RentedScooters { get; set; }
    }
}
