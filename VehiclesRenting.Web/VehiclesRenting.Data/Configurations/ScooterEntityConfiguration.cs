namespace VehiclesRenting.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class ScooterEntityConfiguration : IEntityTypeConfiguration<Scooter>
    {
        public void Configure(EntityTypeBuilder<Scooter> builder)
        {
            builder
                .Property(s => s.CreatedOn)
                .HasDefaultValue(DateTime.UtcNow);

            builder
                .HasOne(s => s.Category)
                .WithMany(c => c.Scooters)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(s => s.Agent)
                .WithMany(a => a.ManagedScooters)
                .HasForeignKey(s => s.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(s => s.PricePerDay)
                .HasPrecision(18, 2);

            builder.HasData(this.GenerateScooters());
        }

        private Scooter[] GenerateScooters()
        {
            ICollection<Scooter> scooters = new List<Scooter>();

            Scooter scooter;

            scooter = new Scooter()
            {
                Brand = "Xiomi",
                CurrentAddress = "Silistra, Center",
                PricePerDay = 12,
                ImageUrl =
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Xiaomi_M365.jpg/1200px-Xiaomi_M365.jpg",
                AgentId = Guid.Parse("B18534F1-C32B-401F-B740-6035CB456174"),
                RenterId = Guid.Parse("3D2D9E6D-038B-4FF5-90B8-4EEBC4C48426"),
                CategoryId = 3,
            };
            scooters.Add(scooter);

            scooter = new Scooter()
            {
                Brand = "E-scooter",
                CurrentAddress = "Silistra, North",
                PricePerDay = 7,
                ImageUrl =
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/2/29/Elektrische-tretroller.jpg/800px-Elektrische-tretroller.jpg",
                AgentId = Guid.Parse("B18534F1-C32B-401F-B740-6035CB456174"),
                CategoryId = 3,
            };
            scooters.Add(scooter);

            scooter = new Scooter()
            {
                Brand = "Inmotion",
                CurrentAddress = "Silistra, West",
                PricePerDay = 9,
                ImageUrl =
                    "https://cdn.shopify.com/s/files/1/0021/7389/4702/products/LeMotion-Web-1.jpg?v=1636106454",
                AgentId = Guid.Parse("B18534F1-C32B-401F-B740-6035CB456174"),
                CategoryId = 3,
            };
            scooters.Add(scooter);

            return scooters.ToArray();
        }
    }
}
