namespace Vehicle2Go.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models.Vehicle;

    public class TruckEntityConfiguration : IEntityTypeConfiguration<Truck>
    {
        public void Configure(EntityTypeBuilder<Truck> builder)
        {
            builder
                .HasOne(t => t.Category)
                .WithMany(c => c.Trucks)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Agent)
                .WithMany(a => a.OwnedTrucks)
                .HasForeignKey(t => t.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(t => t.PricePerDay)
                .HasPrecision(18, 2);

            builder.Property(t => t.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.IsActive)
                .HasDefaultValue(true);

            builder.HasData(this.GenerateTrucks());
        }
        private Truck[] GenerateTrucks()
        {
            ICollection<Truck> trucks = new HashSet<Truck>();

            Truck truck;

            truck = new Truck
            {
                Brand = "Volvo",
                Model = "FH 540",
                RegistrationNumber = "P6558PT",
                Address = "Ruse",
                PricePerDay = 180,
                Color = "Green",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b9/Volvo_FH_540_at_IAA_2014._Free_images_Spielvogel.jpg/1200px-Volvo_FH_540_at_IAA_2014._Free_images_Spielvogel.jpg",
                CategoryId = 1,
                AgentId = Guid.Parse("8af114de-010e-4b30-8920-9064facf5ae1"),
            };
            trucks.Add(truck);

            truck = new Truck
            {
                Brand = "Scania",
                Model = "R730",
                RegistrationNumber = "CB7878CC",
                Address = "Sofia",
                PricePerDay = 230,
                Color = "Red",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2b/Scania_R730.jpg",
                CategoryId = 3,
                AgentId = Guid.Parse("8af114de-010e-4b30-8920-9064facf5ae1"),
            };
            trucks.Add(truck);

            return trucks.ToArray();
        }
    }
}
