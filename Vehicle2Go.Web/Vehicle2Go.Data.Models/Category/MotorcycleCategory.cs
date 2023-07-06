namespace Vehicle2Go.Data.Models.Category
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.CategoryConstants;

    public class MotorcycleCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<Motorcycle> Motorcycles { get; set; } = new List<Motorcycle>();
    }
}
