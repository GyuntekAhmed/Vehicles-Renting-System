namespace Vehicle2Go.Web.ViewModels.Vehicle
{
    using System.ComponentModel.DataAnnotations;

    public class VehiclePreDeleteDetailsViewModel
    {
        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; } = null!;

        public string Address { get; set; } = null!;

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;
    }
}
