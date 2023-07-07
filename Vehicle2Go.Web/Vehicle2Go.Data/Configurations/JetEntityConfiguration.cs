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

            builder.HasData(this.GenerateJets());
        }

        private Jet[] GenerateJets()
        {
            ICollection<Jet> jets = new HashSet<Jet>();

            Jet jet;

            jet = new Jet
            {
                Brand = "Yamaha",
                Model = "WaveRunner",
                RegistrationNumber = "B1551AK",
                Address = "Varna, Black Sea",
                PricePerDay = 50,
                Color = "Black",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/7/7d/2020_Yamaha_FX_SVHO_WaveRunner.jpg",
                CategoryId = 1,
                AgentId = Guid.Parse("EDE9E369-919D-40A2-A20E-30C1F97FB663"),
                RenterId = Guid.Parse("C618F492-937F-4907-F243-08DB7EF8F1D7"),
            };
            jets.Add(jet);

            jet = new Jet
            {
                Brand = "Kawasaki",
                Model = "550",
                RegistrationNumber = "A1660AA",
                Address = "Burgas, Black Sea",
                PricePerDay = 55,
                Color = "Red",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/5/5f/1985_Kawasaki_550_Jet_Ski%C2%AE.jpg",
                CategoryId = 2,
                AgentId = Guid.Parse("EDE9E369-919D-40A2-A20E-30C1F97FB663"),
            };
            jets.Add(jet);

            jet = new Jet
            {
                Brand = "Honda",
                Model = "Aquatrax",
                RegistrationNumber = "B3330PT",
                Address = "Varna, Black Sea",
                PricePerDay = 55,
                Color = "Red",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/59/Honda_Aqua_Trax_F-12x_turbo.jpg/640px-Honda_Aqua_Trax_F-12x_turbo.jpg",
                CategoryId = 2,
                AgentId = Guid.Parse("EDE9E369-919D-40A2-A20E-30C1F97FB663"),
            };
            jets.Add(jet);

            return jets.ToArray();
        }
    }
}
