namespace VehiclesRenting.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class CarEntityConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder
                .Property(c => c.CreatedOn)
                .HasDefaultValue(DateTime.UtcNow);

            builder
                .HasOne(c => c.Category)
                .WithMany(c => c.Cars)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.Agent)
                .WithMany(a => a.ManagedCars)
                .HasForeignKey(c => c.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(c => c.PricePerDay)
                .HasPrecision(18, 2);

            builder.HasData(this.GenerateCars());
        }

        private Car[] GenerateCars()
        {
            ICollection<Car> cars = new List<Car>();

            Car car;

            car = new Car()
            {
                Brand = "Hyundai",
                Model = "Santa fe",
                RegistrationNumber = "CC1835AX",
                CurrentAddress = "Silistra, Center",
                PricePerDay = 40,
                Color = "Black",
                ImageUrl =
                    "https://upload.wikimedia.org/wikipedia/commons/4/4b/Hyundai_Santa_Fe_%28TM%29_PHEV_FL_IMG_6648.jpg",
                AgentId = Guid.Parse("B18534F1-C32B-401F-B740-6035CB456174"),
                RenterId = Guid.Parse("3D2D9E6D-038B-4FF5-90B8-4EEBC4C48426"),
                CategoryId = 1,
            };
            cars.Add(car);

            car = new Car()
            {
                Brand = "Mercedes",
                Model = "S-Class",
                RegistrationNumber = "CB0832AP",
                CurrentAddress = "Silistra, Center",
                PricePerDay = 70,
                Color = "White",
                ImageUrl =
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/5/55/Mercedes-Benz_W223_IMG_6663.jpg/1200px-Mercedes-Benz_W223_IMG_6663.jpg",
                AgentId = Guid.Parse("B18534F1-C32B-401F-B740-6035CB456174"),
                CategoryId = 1,
            };
            cars.Add(car);

            car = new Car()
            {
                Brand = "Ford Mustang",
                Model = "GT500 Shelby",
                RegistrationNumber = "CC0000CC",
                CurrentAddress = "Silistra, East",
                PricePerDay = 200,
                Color = "Gray",
                ImageUrl =
                    "https://upload.wikimedia.org/wikipedia/commons/f/f6/1967_Ford_Mustang_Shelby_GT-500_Eleanor.jpg",
                AgentId = Guid.Parse("B18534F1-C32B-401F-B740-6035CB456174"),
                CategoryId = 1,
            };
            cars.Add(car);

            return cars.ToArray();
        }
    }
}
