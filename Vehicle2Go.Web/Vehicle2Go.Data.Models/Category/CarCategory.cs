namespace Vehicle2Go.Data.Models.Category
{
    using System.ComponentModel.DataAnnotations;
    using Vehicle2Go.Data.Models.Vehicle;
    using static Common.EntityValidationConstants.CategoryConstants;

    public class CarCategory
    {
        public CarCategory()
        {
            this.Cars = new HashSet<Car>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<Car> Cars { get; set; }
    }
}
