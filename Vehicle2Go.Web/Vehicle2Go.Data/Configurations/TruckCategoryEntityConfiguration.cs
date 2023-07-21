namespace Vehicle2Go.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models.Category;

    public class TruckCategoryEntityConfiguration : IEntityTypeConfiguration<TruckCategory>
    {
        public void Configure(EntityTypeBuilder<TruckCategory> builder)
        {
            builder.HasData(this.GenerateCategories());
        }

        private TruckCategory[] GenerateCategories()
        {
            ICollection<TruckCategory> categories = new HashSet<TruckCategory>();

            TruckCategory category;

            category = new TruckCategory()
            {
                Id = 1,
                Name = "Small Truck",
            };
            categories.Add(category);

            category = new TruckCategory()
            {
                Id = 2,
                Name = "Medium Truck",
            };
            categories.Add(category);

            category = new TruckCategory()
            {
                Id = 3,
                Name = "Big Truck",
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
