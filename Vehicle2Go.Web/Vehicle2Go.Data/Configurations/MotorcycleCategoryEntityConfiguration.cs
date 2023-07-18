namespace Vehicle2Go.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models.Category;

    public class MotorcycleCategoryEntityConfiguration : IEntityTypeConfiguration<MotorcycleCategory>
    {
        public void Configure(EntityTypeBuilder<MotorcycleCategory> builder)
        {
            builder.HasData(this.GenerateCategories());
        }

        private MotorcycleCategory[] GenerateCategories()
        {
            ICollection<MotorcycleCategory> categories = new HashSet<MotorcycleCategory>();

            MotorcycleCategory category;

            category = new MotorcycleCategory
            {
                Id = 1,
                Name = "Sport",
            };
            categories.Add(category);

            category = new MotorcycleCategory
            {
                Id = 2,
                Name = "Motocross",
            };
            categories.Add(category);

            category = new MotorcycleCategory
            {
                Id = 3,
                Name = "Street",
            };
            categories.Add(category);

            category = new MotorcycleCategory
            {
                Id = 4,
                Name = "Chopper",
            };
            categories.Add(category);

            category = new MotorcycleCategory
            {
                Id = 5,
                Name = "Scooter",
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
