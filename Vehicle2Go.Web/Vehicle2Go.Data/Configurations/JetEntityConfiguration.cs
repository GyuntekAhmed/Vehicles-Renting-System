namespace Vehicle2Go.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models.Vehicle;

    public class JetEntityConfiguration : IEntityTypeConfiguration<Jet>
    {
        public void Configure(EntityTypeBuilder<Jet> builder)
        {
            builder
                .HasOne(j => j.Category)
                .WithMany(c => c.Jets)
                .HasForeignKey(j => j.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(j => j.Agent)
                .WithMany(a => a.OwnedJets)
                .HasForeignKey(j => j.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(j => j.PricePerDay)
                .HasPrecision(18, 2);

            builder.Property(j => j.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.IsActive)
                .HasDefaultValue(true);

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
                    AgentId = Guid.Parse("6FC60999-8FC8-46B6-A131-897EDD45A5F0"),
                    RenterId = Guid.Parse("405004AA-8BD0-4407-A506-89E514C30FAF"),
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
                    AgentId = Guid.Parse("6FC60999-8FC8-46B6-A131-897EDD45A5F0"),
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
                    AgentId = Guid.Parse("6FC60999-8FC8-46B6-A131-897EDD45A5F0"),
                };
                jets.Add(jet);

                return jets.ToArray();
            }
        }
    }
