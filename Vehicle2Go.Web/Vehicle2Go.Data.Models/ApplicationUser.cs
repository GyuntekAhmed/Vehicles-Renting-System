namespace Vehicle2Go.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.RentedCars = new List<Car>();
            this.RentedMotorcycles = new List<Motorcycle>();
            this.RentedJets = new List<Jet>();
            this.RentedYachts = new List<Yacht>();
        }

        public ICollection<Car> RentedCars { get; set; }
        public ICollection<Motorcycle> RentedMotorcycles { get; set; }
        public ICollection<Jet> RentedJets{ get; set; }
        public ICollection<Yacht> RentedYachts { get; set; }
    }
}
