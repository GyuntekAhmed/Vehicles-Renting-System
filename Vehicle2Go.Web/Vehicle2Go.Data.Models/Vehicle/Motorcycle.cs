﻿namespace Vehicle2Go.Data.Models.Vehicle
{
    using System.ComponentModel.DataAnnotations;

    using Agent;
    using Category;
    using User;

    using static Common.EntityValidationConstants.VehicleConstants;

    public class Motorcycle
    {
        public Motorcycle()
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
        [MaxLength(RegistrationMaxLength)]
        public string RegistrationNumber { get; set; } = null!;

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = null!;

        public decimal PricePerDay { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [MaxLength(ColorMaxLength)]
        public string Color { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        public int CategoryId { get; set; }

        public MotorcycleCategory Category { get; set; } = null!;

        public Guid AgentId { get; set; }

        public Agent Agent { get; set; } = null!;

        public Guid? RenterId { get; set; }

        public ApplicationUser? Renter { get; set; }
    }
}
