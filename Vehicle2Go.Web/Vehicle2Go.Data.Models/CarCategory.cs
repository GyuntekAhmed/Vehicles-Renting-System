namespace Vehicle2Go.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.CategoryConstants;

    public class CarCategory
    {
        [Key]
        public int Id { get; set; }

        [Required] [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}