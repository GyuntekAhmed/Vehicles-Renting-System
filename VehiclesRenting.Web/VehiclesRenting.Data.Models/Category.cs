namespace VehiclesRenting.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static VehiclesRenting.Common.Constants.EntityValidationConstants.Category;

    public class Category
    {
        public Category()
        {
            this.Cars = new List<Car>();
            this.Motorcycles = new List<Motorcycle>();
            this.Scooters = new List<Scooter>();
            this.Jets = new List<Jet>();
            this.Yachts = new List<Yacht>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Car> Cars{ get; set; }

        public virtual ICollection<Motorcycle> Motorcycles { get; set; }

        public virtual ICollection<Scooter> Scooters { get; set; }

        public virtual ICollection<Jet> Jets { get; set; }

        public virtual ICollection<Yacht> Yachts { get; set; }
    }
}