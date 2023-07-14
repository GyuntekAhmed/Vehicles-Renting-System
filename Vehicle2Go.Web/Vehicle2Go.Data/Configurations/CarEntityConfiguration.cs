namespace Vehicle2Go.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models.Vehicle;

    public class CarEntityConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder
                .HasOne(c => c.Category)
                .WithMany(c => c.Cars)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Agent)
                .WithMany(a => a.OwnedCars)
                .HasForeignKey(c => c.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(c => c.PricePerDay)
                .HasPrecision(18, 2);

            builder.Property(c => c.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder.HasData(this.GenerateCars());
        }

        private Car[] GenerateCars()
        {
            ICollection<Car> cars = new HashSet<Car>();

            Car car;

            car = new Car
            {
                Brand = "Hyundai",
                Model = "Santa fe",
                RegistrationNumber = "CC1835AK",
                Address = "Silistra, Center",
                PricePerDay = 80,
                Color = "White",
                ImageUrl =
                    "https://upload.wikimedia.org/wikipedia/commons/f/fb/2010_Hyundai_Santa_Fe_GLS_--_09-24-2010.jpg",
                CategoryId = 1,
                AgentId = Guid.Parse("104305C7-49AC-4DC4-9D12-650F57703082"),
                RenterId = Guid.Parse("20F4A224-CF5B-4FB7-EBB9-08DB84A80400"),
            };
            cars.Add(car);

            car = new Car
            {
                Brand = "BMW",
                Model = "E64",
                RegistrationNumber = "CA1122PT",
                Address = "Sofia, West",
                PricePerDay = 110,
                Color = "Black",
                ImageUrl =
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bb/BMW_E64_M6_Convertible.JPG/800px-BMW_E64_M6_Convertible.JPG",
                CategoryId = 6,
                AgentId = Guid.Parse("104305C7-49AC-4DC4-9D12-650F57703082"),
            };
            cars.Add(car);

            car = new Car
            {
                Brand = "Mercedes",
                Model = "S-Class",
                RegistrationNumber = "B6555AH",
                Address = "Varna, East",
                PricePerDay = 90,
                Color = "Black",
                ImageUrl =
                    "https://upload.wikimedia.org/wikipedia/commons/3/35/Mercedes-Benz_S_320_CDI_4MATIC_L_%28V_221%29_%E2%80%93_Frontansicht_%281%29%2C_30._August_2011%2C_D%C3%BCsseldorf.jpg",
                CategoryId = 2,
                AgentId = Guid.Parse("104305C7-49AC-4DC4-9D12-650F57703082"),
            };
            cars.Add(car);

            car = new Car
            {
                Brand = "Ford",
                Model = "Mustang",
                RegistrationNumber = "PB7500AT",
                Address = "Plovdiv, North",
                PricePerDay = 130,
                Color = "Blue",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1f/2019_Ford_Mustang_GT_5.0_facelift.jpg/1200px-2019_Ford_Mustang_GT_5.0_facelift.jpg",
                CategoryId = 3,
                AgentId = Guid.Parse("104305C7-49AC-4DC4-9D12-650F57703082"),
            };
            cars.Add(car);

            car = new Car
            {
                Brand = "Hummer",
                Model = "H2-Limousine",
                RegistrationNumber = "C1300AH",
                Address = "Sofia, Center",
                PricePerDay = 200,
                Color = "White",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/7/72/Hummer_limousine_2017.jpg",
                CategoryId = 7,
                AgentId = Guid.Parse("104305C7-49AC-4DC4-9D12-650F57703082"),
            };
            cars.Add(car);

            return cars.ToArray();
        }
    }
}
