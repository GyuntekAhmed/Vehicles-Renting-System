namespace Vehicle2Go.Data.Configurations.CategoryConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models.Category;
    
    public class JetCategoryConfiguration : IEntityTypeConfiguration<JetCategory>
    {
        public void Configure(EntityTypeBuilder<JetCategory> builder)
        {
            builder.HasData(this.GenerateCategories());
        }

        private JetCategory[] GenerateCategories()
        {
            ICollection<JetCategory> categories = new HashSet<JetCategory>();

            JetCategory jetCategory;

            jetCategory = new JetCategory
            {
                Id = 1,
                Name = "Luxury",
            };
            categories.Add(jetCategory);

            jetCategory = new JetCategory
            {
                Id = 2,
                Name = "Sport",
            };
            categories.Add(jetCategory);

            return categories.ToArray();
        }
    }
}
