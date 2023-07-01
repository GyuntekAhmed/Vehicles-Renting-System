namespace VehiclesRenting.Web.ViewModels.Vehicles
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Category;

    using static Common.Constants.EntityValidationConstants.VehichleConstants;

    public class AddVehiclesViewModel
    {
        [Required]
        [StringLength(BrandMaxLength, MinimumLength = BrandMinLength)]
        public string Brand { get; set; } = null!;

        public string? Model { get; set; }

        [Display(Name = "Registration Number")]
        public string? RegistrationNumber { get; set; }

        [Required]
        [StringLength(CurrentAddressMaxLength, MinimumLength = CurrentAddressMinLength)]
        [Display(Name = "Address")]
        public string CurrentAddress { get; set; } = null!;

        [Range(typeof(decimal), PricePerDayMinValue, PricePerDayMaxValue)]
        [Display(Name = "Price")]
        public decimal PricePerDay { get; set; }

        [Display(Name = "Added on")]
        public DateTime CreatedOn { get; set; }

        [Required]
        [StringLength(ImageUrlMaxLength, MinimumLength = ImageUrlMinLength)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; } = null!;

        public string? Color { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<VehicleCategoryViewModel> Categories { get; set; } = new List<VehicleCategoryViewModel>();
    }
}
