namespace Vehicle2Go.Web.ViewModels.Vehicle
{
    using System.ComponentModel.DataAnnotations;

    public class VehicleAllViewModel
    {
        public string Id { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Color { get; set; } = null!;

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        [Display(Name = "Daily price")]
        public decimal PricePerDay { get; set; }

        [Display(Name = "Is Rented")]
        public bool IsRented { get; set; }
    }
}
