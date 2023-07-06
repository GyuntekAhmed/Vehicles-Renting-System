namespace Vehicle2Go.Data.Models.Category
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.CategoryConstants;

    public class YachtCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<Yacht> Yachts { get; set; } = new List<Yacht>();
    }
}
