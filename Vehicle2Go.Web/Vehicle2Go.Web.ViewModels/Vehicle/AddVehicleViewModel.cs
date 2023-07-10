namespace Vehicle2Go.Web.ViewModels.Vehicle
{
    using System.ComponentModel.DataAnnotations;

    using Category;

    using static Common.EntityValidationConstants.VehicleConstants;

    public class AddVehicleViewModel
    {
        public AddVehicleViewModel()
        {
            this.Categories = new HashSet<VehicleSelectCategoryViewModel>();
        }

        [Required]
        [StringLength(BrandMaxLength, MinimumLength = BrandMinLength)]
        public string Brand { get; set; } = null!;

        [Required]
        [StringLength(ModelMaxLength, MinimumLength = ModelMinLength)]
        public string Model { get; set; } = null!;

        [Required]
        [StringLength(RegistrationMaxLength, MinimumLength = RegistrationMinLength)]
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; } = null!;

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; } = null!;

        [Range(typeof(decimal), PricePerDayMinValue, PricePerDayMaxValue)]
        [Display(Name = "Daily Price")]
        public decimal PricePerDay { get; set; }

        [Required]
        [StringLength(ColorMaxLength, MinimumLength = ColorMinLength)]
        public string Color { get; set; } = null!;

        [Required]
        [StringLength(ImageUrlMaxLength, MinimumLength = ImageUrlMinLength)]
        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<VehicleSelectCategoryViewModel> Categories { get; set; }
    }
}
