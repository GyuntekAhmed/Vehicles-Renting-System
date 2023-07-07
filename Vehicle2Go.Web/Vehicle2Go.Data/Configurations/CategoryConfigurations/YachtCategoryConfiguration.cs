namespace Vehicle2Go.Data.Configurations.CategoryConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models.Category;

    public class YachtCategoryConfiguration : IEntityTypeConfiguration<YachtCategory>
    {
        public void Configure(EntityTypeBuilder<YachtCategory> builder)
        {
            builder.HasData(this.GenerateCategories());
        }

        private YachtCategory[] GenerateCategories()
        {
            ICollection<YachtCategory> categories = new HashSet<YachtCategory>();

            YachtCategory category;

            category = new YachtCategory
            {
                Id = 1,
                Name = "Cabin Cruiser",
            };
            categories.Add(category);

            category = new YachtCategory
            {
                Id = 2,
                Name = "Small Yacht",
            };
            categories.Add(category);

            category = new YachtCategory
            {
                Id = 3,
                Name = "Big Yacht",
            };
            categories.Add(category);

            category = new YachtCategory
            {
                Id = 4,
                Name = "Speedboat",
            };
            categories.Add(category);

            category = new YachtCategory
            {
                Id = 5,
                Name = "Windsurfer",
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
