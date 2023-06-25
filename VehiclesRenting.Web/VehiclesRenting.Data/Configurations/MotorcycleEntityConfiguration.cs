namespace VehiclesRenting.Data.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;

    using Models;

    public class MotorcycleEntityConfiguration : IEntityTypeConfiguration<Motorcycle>
    {
        public void Configure(EntityTypeBuilder<Motorcycle> builder)
        {
            builder
                .Property(m => m.CreatedOn)
                .HasDefaultValue(DateTime.UtcNow);

            builder
                .HasOne(m => m.Category)
                .WithMany(c => c.Motorcycles)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(m => m.Agent)
                .WithMany(a => a.ManagedMotorcycles)
                .HasForeignKey(m => m.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(m => m.PricePerDay)
                .HasPrecision(18, 2);

            builder.HasData(this.GenerateMotorcycles());
        }

        private Motorcycle[] GenerateMotorcycles()
        {
            ICollection<Motorcycle> motorcycles = new List<Motorcycle>();

            Motorcycle motorcycle;

            motorcycle = new Motorcycle()
            {
                Brand = "Honda",
                Model = "NR-L",
                RegistrationNumber = "CC3552OB",
                CurrentAddress = "Ruse, Center",
                PricePerDay = 30,
                Color = "Red",
                ImageUrl =
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/0/06/Honda750NR.jpg/1200px-Honda750NR.jpg",
                AgentId = Guid.Parse("B18534F1-C32B-401F-B740-6035CB456174"),
                RenterId = Guid.Parse("3D2D9E6D-038B-4FF5-90B8-4EEBC4C48426"),
                CategoryId = 2,
            };
            motorcycles.Add(motorcycle);

            motorcycle = new Motorcycle()
            {
                Brand = "Suzuki",
                Model = "GSX-R1000K5",
                RegistrationNumber = "CC1550AB",
                CurrentAddress = "Silistra, Center",
                PricePerDay = 75,
                Color = "Blue",
                ImageUrl =
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f6/GSXR1000K5.jpg/640px-GSXR1000K5.jpg",
                AgentId = Guid.Parse("B18534F1-C32B-401F-B740-6035CB456174"),
                CategoryId = 2,
            };
            motorcycles.Add(motorcycle);

            motorcycle = new Motorcycle()
            {
                Brand = "Harley-Davidson",
                Model = "VRSC",
                RegistrationNumber = "CC1200AK",
                CurrentAddress = "Silistra, West",
                PricePerDay = 110,
                Color = "Gray",
                ImageUrl =
                    "https://upload.wikimedia.org/wikipedia/commons/1/13/Harley_5-06.jpg",
                AgentId = Guid.Parse("B18534F1-C32B-401F-B740-6035CB456174"),
                CategoryId = 2,
            };
            motorcycles.Add(motorcycle);

            return motorcycles.ToArray();
        }
    }
}
