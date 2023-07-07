namespace Vehicle2Go.Data.Configurations.CategoryConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models.Category;
    
    public class CarCategoryConfiguration : IEntityTypeConfiguration<CarCategory>
    {
        public void Configure(EntityTypeBuilder<CarCategory> builder)
        {
            builder.HasData(this.GenerateCategories());
        }

        private CarCategory[] GenerateCategories()
        {
            ICollection<CarCategory> categories = new HashSet<CarCategory>();

            CarCategory carCategory;

            carCategory = new CarCategory
            {
                Id = 1,
                Name = "Crossover",
            };
            categories.Add(carCategory);

            carCategory = new CarCategory
            {
                Id = 2,
                Name = "Sedan",
            };
            categories.Add(carCategory);

            carCategory = new CarCategory
            {
                Id = 3,
                Name = "Sport",
            };
            categories.Add(carCategory);

            carCategory = new CarCategory
            {
                Id = 4,
                Name = "Coupe",
            };
            categories.Add(carCategory);

            carCategory = new CarCategory
            {
                Id = 5,
                Name = "Hatchback",
            };
            categories.Add(carCategory);

            carCategory = new CarCategory
            {
                Id = 6,
                Name = "Cabriolet",
            };
            categories.Add(carCategory);

            carCategory = new CarCategory
            {
                Id = 7,
                Name = "Limousine",
            };
            categories.Add(carCategory);

            return categories.ToArray();
        }
    }
}
