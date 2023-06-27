namespace VehiclesRenting.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static VehiclesRenting.Common.Constants.EntityValidationConstants.Agent;

    public class Agent
    {
        public Agent()
        {
            this.Id = Guid.NewGuid();
            this.ManagedCars = new List<Car>();
            this.ManagedMotorcycles = new List<Motorcycle>();
            this.ManagedScooters = new List<Scooter>();
        }

        [Key]
        public Guid Id { get; set; }
        
        
        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        public string? Address { get; set; }
        

        public Guid UserId { get; set; }

        public virtual User User { get; set; } = null!;

        public virtual ICollection<Car> ManagedCars { get; set; }

        public virtual ICollection<Motorcycle> ManagedMotorcycles { get; set; }

        public virtual ICollection<Scooter> ManagedScooters { get; set; }
    }
}
