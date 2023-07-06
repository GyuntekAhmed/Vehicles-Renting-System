namespace Vehicle2Go.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class JetEntityConfiguration : IEntityTypeConfiguration<Jet>
    {
        public void Configure(EntityTypeBuilder<Jet> builder)
        {
            builder
                .HasOne(j => j.Category)
                .WithMany(c => c.Jets)
                .HasForeignKey(j => j.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(j => j.Agent)
                .WithMany(a => a.ManagedJets)
                .HasForeignKey(j => j.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(j => j.Renter)
                .WithMany(r => r.RentedJets)
                .HasForeignKey(j => j.RenterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(j => j.PricePerDay)
                .HasPrecision(18, 2);

            builder.Property(j => j.CreatedOn)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
