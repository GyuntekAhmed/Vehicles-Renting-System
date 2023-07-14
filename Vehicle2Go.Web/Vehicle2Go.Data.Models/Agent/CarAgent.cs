namespace Vehicle2Go.Data.Models.Agent
{
    using System.ComponentModel.DataAnnotations;

    using Vehicle;
    using User;

    using static Common.EntityValidationConstants.AgentConstants;

    public class CarAgent
    {
        public CarAgent()
        {
            this.Id = Guid.NewGuid();
            this.OwnedCars = new HashSet<Car>();
        }
        public Guid Id { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = null!;

        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; } = null!;

        public ICollection<Car> OwnedCars { get; set; }
    }
}
