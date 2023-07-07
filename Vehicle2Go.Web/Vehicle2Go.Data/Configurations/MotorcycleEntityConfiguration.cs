namespace Vehicle2Go.Data.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;

    using Models;

    public class MotorcycleEntityConfiguration : IEntityTypeConfiguration<Motorcycle>
    {
        public void Configure(EntityTypeBuilder<Motorcycle> builder)
        {
            builder
                .HasOne(m =>m.Category)
                .WithMany(c => c.Motorcycles)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(m => m.Agent)
                .WithMany(a => a.ManagedMotorcycles)
                .HasForeignKey(m => m.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(m => m.Renter)
                .WithMany(r => r.RentedMotorcycles)
                .HasForeignKey(m => m.RenterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(m => m.PricePerDay)
                .HasPrecision(18, 2);

            builder.Property(m => m.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder.HasData(this.GenerateMotorcycles());
        }

        private Motorcycle[] GenerateMotorcycles()
        {
            ICollection<Motorcycle> motorcycles = new HashSet<Motorcycle>();

            Motorcycle motorcycle;

            motorcycle = new Motorcycle
            {
                Brand = "BMW",
                Model = "K1300R",
                RegistrationNumber = "CC4565KT",
                Address = "Silistra, Center",
                PricePerDay = 45,
                Color = "Black",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/c/c9/BMW_K1300R_Rennes.jpg",
                CategoryId = 3,
                AgentId = Guid.Parse("EDE9E369-919D-40A2-A20E-30C1F97FB663"),
                RenterId = Guid.Parse("C618F492-937F-4907-F243-08DB7EF8F1D7"),
            };
            motorcycles.Add(motorcycle);

            motorcycle = new Motorcycle
            {
                Brand = "Suzuki",
                Model = "Hayabusa",
                RegistrationNumber = "P2112KH",
                Address = "Ruse, East",
                PricePerDay = 40,
                Color = "Gray",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/7/71/1999modelzwart.JPG",
                CategoryId = 1,
                AgentId = Guid.Parse("EDE9E369-919D-40A2-A20E-30C1F97FB663"),
            };
            motorcycles.Add(motorcycle);

            motorcycle = new Motorcycle
            {
                Brand = "Honda",
                Model = "XLV750R",
                RegistrationNumber = "A4747AT",
                Address = "Burgas, Center",
                PricePerDay = 35,
                Color = "Red",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/be/Honda_XLV750R%28D%29_1.jpg/1200px-Honda_XLV750R%28D%29_1.jpg",
                CategoryId = 2,
                AgentId = Guid.Parse("EDE9E369-919D-40A2-A20E-30C1F97FB663"),
            };
            motorcycles.Add(motorcycle);

            return motorcycles.ToArray();
        }
    }
}
