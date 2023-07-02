namespace VehiclesRenting.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class JetEntityConfiguration : IEntityTypeConfiguration<Jet>
    {
        public void Configure(EntityTypeBuilder<Jet> builder)
        {
            builder.Property(j => j.CategoryId)
                .HasDefaultValue(4);

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
                .Property(j => j.PricePerDay)
                .HasPrecision(18, 2);

            builder.HasData(this.GenerateJets());
        }

        private Jet[] GenerateJets()
        {
            ICollection<Jet> jets = new List<Jet>();

            Jet jet;

            jet = new Jet()
            {
                Brand = "Yamaha",
                Model = "Wave Runner",
                CurrentAddress = "Varna, Black Sea",
                PricePerDay = 50,
                Color = "Black",
                ImageUrl =
                    "https://upload.wikimedia.org/wikipedia/commons/7/7d/2020_Yamaha_FX_SVHO_WaveRunner.jpg",
                AgentId = Guid.Parse("8ED4EAA3-738C-49A4-9CF8-874903DED0BB"),
                RenterId = Guid.Parse("C42EF5D1-0C67-4DC2-9467-EC9947BAA83F"),
            };
            jets.Add(jet);

            jet = new Jet()
            {
                Brand = "Honda",
                Model = "PWC",
                CurrentAddress = "Burgas",
                PricePerDay = 60,
                Color = "Red",
                ImageUrl =
                    "https://getmyboat-user-images1.imgix.net/images/626ebcd126768/-processed.jpg",
                AgentId = Guid.Parse("8ED4EAA3-738C-49A4-9CF8-874903DED0BB"),
            };
            jets.Add(jet);

            jet = new Jet()
            {
                Brand = "Yamaha",
                Model = "Superjet 701",
                CurrentAddress = "Balchik",
                PricePerDay = 45,
                Color = "Blue",
                ImageUrl =
                    "https://d1kqllve43agrl.cloudfront.net/imgs/Yamaha-superjet-701-bj-2008-17754.jpeg",
                AgentId = Guid.Parse("8ED4EAA3-738C-49A4-9CF8-874903DED0BB"),
            };
            jets.Add(jet);

            return jets.ToArray();
        }
    }
}
