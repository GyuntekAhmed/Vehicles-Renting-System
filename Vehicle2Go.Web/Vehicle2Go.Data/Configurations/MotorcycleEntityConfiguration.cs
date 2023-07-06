﻿namespace Vehicle2Go.Data.Configurations
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
        }
    }
}