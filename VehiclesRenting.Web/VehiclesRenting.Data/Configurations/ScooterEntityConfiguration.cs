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
        }
    }
}
