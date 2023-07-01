namespace VehiclesRenting.Data.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;

    using Models;

    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(this.GenerateCategories());
        }

        private Category[] GenerateCategories()
        {
            ICollection<Category> categories = new List<Category>();

            Category category;

            category = new Category()
            {
                Id = 1,
                Name = "Car"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 2,
                Name = "Motorcycle"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 3,
                Name = "Scooter"
            };

            category = new Category()
            {
                Id = 4,
                Name = "Jet"
            };

            category = new Category()
            {
                Id = 5,
                Name = "Yacht"
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
