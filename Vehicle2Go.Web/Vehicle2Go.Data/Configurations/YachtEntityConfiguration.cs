namespace Vehicle2Go.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class YachtEntityConfiguration : IEntityTypeConfiguration<Yacht>
    {
        public void Configure(EntityTypeBuilder<Yacht> builder)
        {
            builder
                .HasOne(y => y.Category)
                .WithMany(c => c.Yachts)
                .HasForeignKey(y => y.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(y => y.Agent)
                .WithMany(a => a.ManagedYachts)
                .HasForeignKey(y => y.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(y => y.Renter)
                .WithMany(r => r.RentedYachts)
                .HasForeignKey(y => y.RenterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(y => y.PricePerDay)
                .HasPrecision(18, 2);

            builder.Property(y => y.CreatedOn)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
