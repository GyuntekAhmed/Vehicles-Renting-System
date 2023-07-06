namespace Vehicle2Go.Data.Models.Agent
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.AgentConstants;

    public class CarAgent
    {
        public CarAgent()
        {
            this.Id = Guid.NewGuid();
            this.ManagedCars = new List<Car>();
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

        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<Car> ManagedCars { get; set; }
    }
}
