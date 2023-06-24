namespace VehiclesRenting.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.User;

    // This is custom user class that works with IdentityUser.

    public class User : IdentityUser<Guid>
    {
        public User()
        {
            this.RentedCars = new List<Car>();
            this.RentedMotorcycles = new List<Motorcycle>();
            this.RentedScooters = new List<Scooter>();
        }
        
        [Required]
        [MaxLength(CurrentAddressMaxLength)]
        public string CurrentAddress { get; set; } = null!;

        public virtual ICollection<Car> RentedCars { get; set; }

        public virtual ICollection<Motorcycle> RentedMotorcycles { get; set; }

        public virtual ICollection<Scooter> RentedScooters { get; set; }
    }
}
