namespace Vehicle2Go.Data.Models.Category
{
    using System.ComponentModel.DataAnnotations;

    using Vehicle;

    using static Common.EntityValidationConstants.CategoryConstants;

    public class MotorcycleCategory
    {
        public MotorcycleCategory()
        {
            this.Motorcycles = new HashSet<Motorcycle>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<Motorcycle> Motorcycles { get; set; }
    }
}
