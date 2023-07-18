using Vehicle2Go.Data.Models.Vehicle;

namespace Vehicle2Go.Data.Models.Agent
{
    using System.ComponentModel.DataAnnotations;

    using User;

    using static Common.EntityValidationConstants.AgentConstants;

    public class MotorcycleAgent
    {
        public MotorcycleAgent()
        {
            this.Id = Guid.NewGuid();
            this.OwnedMotorcycles = new HashSet<Motorcycle>();
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

        public ICollection<Motorcycle> OwnedMotorcycles { get; set; }
    }
}
