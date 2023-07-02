namespace VehiclesRenting.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Common.Constants.EntityValidationConstants.VehichleConstants;

    public class Yacht
    {
        public Yacht()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(BrandMaxLength)]
        public string Brand { get; set; } = null!;

        [Required]
        [MaxLength(ModelMaxLength)]
        public string Model { get; set; } = null!;

        [Required]
        [MaxLength(CurrentAddressMaxLength)]
        public string CurrentAddress { get; set; } = null!;

        public decimal PricePerDay { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(ColorMaxLength)]
        public string Color { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;

        public Guid AgentId { get; set; }

        public virtual Agent Agent { get; set; } = null!;

        public Guid? RenterId { get; set; }

        public virtual User? Renter { get; set; }
    }
}
