namespace VehiclesRenting.Data.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;

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
                .HasOne(c => c.Agent)
                .WithMany(a => a.ManagedYachts)
                .HasForeignKey(y => y.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(y => y.PricePerDay)
                .HasPrecision(18, 2);

            builder.HasData(this.GenerateYachts());
        }

        private Yacht[] GenerateYachts()
        {
            ICollection<Yacht> yachts = new List<Yacht>();

            Yacht yacht;

            yacht = new Yacht()
            {
                Brand = "Bavaria",
                Model = "37 Sport",
                CurrentAddress = "Varna",
                PricePerDay = 200,
                Color = "White",
                ImageUrl =
                    "https://images.boatsgroup.com/images/1/95/5/8289505_0_230720220750_1.jpg",
                AgentId = Guid.Parse("8ED4EAA3-738C-49A4-9CF8-874903DED0BB"),
                RenterId = Guid.Parse("C42EF5D1-0C67-4DC2-9467-EC9947BAA83F"),
                CategoryId = 5,
            };
            yachts.Add(yacht);

            yacht = new Yacht()
            {
                Brand = "Bavaria",
                Model = "C42 Freedom",
                CurrentAddress = "Burgas",
                PricePerDay = 220,
                Color = "White",
                ImageUrl =
                    "https://www.sailionian.com/wp-content/uploads/2020/05/c42-ex-01.jpg",
                AgentId = Guid.Parse("8ED4EAA3-738C-49A4-9CF8-874903DED0BB"),
                CategoryId = 5,
            };
            yachts.Add(yacht);

            yacht = new Yacht()
            {
                Brand = "Hanse",
                Model = "675",
                CurrentAddress = "Varna",
                PricePerDay = 180,
                Color = "Gray",
                ImageUrl =
                    "https://img.yachtall.com/image-sale-boat/hanse-675-huge-203059a1l5rng9zti.jpg",
                AgentId = Guid.Parse("8ED4EAA3-738C-49A4-9CF8-874903DED0BB"),
                CategoryId = 5,
            };
            yachts.Add(yacht);

            return yachts.ToArray();
        }
    }
}
