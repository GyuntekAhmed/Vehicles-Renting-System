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

            builder.HasData(this.GenerateYachts());
        }

        private Yacht[] GenerateYachts()
        {
            ICollection<Yacht> yachts = new HashSet<Yacht>();

            Yacht yacht;

            yacht = new Yacht
            {
                Brand = "Tiara",
                Model = "43 LE",
                RegistrationNumber = "A1221AX",
                Address = "Burgas",
                PricePerDay = 130,
                Color = "White",
                ImageUrl = "https://www.tiarayachts.com/media/wysiwyg/new-43le/43le-banner1-1600x1066.jpg",
                CategoryId = 1,
                AgentId = Guid.Parse("EDE9E369-919D-40A2-A20E-30C1F97FB663"),
                RenterId = Guid.Parse("C618F492-937F-4907-F243-08DB7EF8F1D7"),
            };
            yachts.Add(yacht);

            yacht = new Yacht
            {
                Brand = "Runabout",
                Model = "Hacker-Craft",
                RegistrationNumber = "B3553PT",
                Address = "Varna",
                PricePerDay = 180,
                Color = "Red",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/2/29/Hacker_Runabout_2010.jpg",
                CategoryId = 2,
                AgentId = Guid.Parse("EDE9E369-919D-40A2-A20E-30C1F97FB663"),
            };
            yachts.Add(yacht);

            return yachts.ToArray();
        }
    }
}
