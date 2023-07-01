namespace VehiclesRenting.Web.ViewModels.Car
{
    using System.ComponentModel.DataAnnotations;

    public class AllCarsViewModel
    {
        public string Brand { get; set; } = null!;
        
        public string Model { get; set; } = null!;
        
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; } = null!;
        
        [Display(Name = "Address")]
        public string CurrentAddress { get; set; } = null!;

        [Display(Name = "Price")]
        public decimal PricePerDay { get; set; }

        [Display(Name = "Added on")]
        public DateTime CreatedOn { get; set; }
        
        [Display(Name = "Image")]
        public string ImageUrl { get; set; } = null!;
        
        public string Color { get; set; } = null!;
    }
}
