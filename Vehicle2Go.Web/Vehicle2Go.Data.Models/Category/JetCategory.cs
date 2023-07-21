namespace Vehicle2Go.Data.Models.Category
{
    using System.ComponentModel.DataAnnotations;

    using Vehicle;

    using static Common.EntityValidationConstants.CategoryConstants;

    public class JetCategory
    {
        public JetCategory()
        {
            this.Jets = new HashSet<Jet>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<Jet> Jets { get; set; }
    }
}
