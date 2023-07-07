namespace Vehicle2Go.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.AgentConstants;

    public class Agent
    {
        public Agent()
        {
            this.Id = Guid.NewGuid();
            this.ManagedCars = new List<Car>();
            this.ManagedMotorcycles = new List<Motorcycle>();
            this.ManagedJets = new List<Jet>();
            this.ManagedYachts = new List<Yacht>();
        }

        [Key]
        public Guid Id { get; set; }


        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = null!;


        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; } = null!;

        public ICollection<Car> ManagedCars { get; set; }

        public ICollection<Motorcycle> ManagedMotorcycles { get; set; }
        public ICollection<Jet> ManagedJets { get; set; }
        public ICollection<Yacht> ManagedYachts { get; set; }
    }
}