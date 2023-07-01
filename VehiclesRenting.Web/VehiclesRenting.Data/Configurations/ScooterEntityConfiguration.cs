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
                AgentId = Guid.Parse("8ED4EAA3-738C-49A4-9CF8-874903DED0BB"),
                RenterId = Guid.Parse("C42EF5D1-0C67-4DC2-9467-EC9947BAA83F"),
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
                AgentId = Guid.Parse("8ED4EAA3-738C-49A4-9CF8-874903DED0BB"),
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
                AgentId = Guid.Parse("8ED4EAA3-738C-49A4-9CF8-874903DED0BB"),
                CategoryId = 3,
            };
            scooters.Add(scooter);

            return scooters.ToArray();
        }
    }
}
