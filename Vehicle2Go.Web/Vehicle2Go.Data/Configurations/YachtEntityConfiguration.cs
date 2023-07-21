﻿namespace Vehicle2Go.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models.Vehicle;

    public class YachtEntityConfiguration : IEntityTypeConfiguration<Yacht>
    {
        public void Configure(EntityTypeBuilder<Yacht> builder)
        {
            builder
                .HasOne(y => y.Category)
                .WithMany(c => c.Yachts)
                .HasForeignKey(y => y.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(y => y.Agent)
                .WithMany(a => a.OwnedYachts)
                .HasForeignKey(y => y.AgentId)
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
                    AgentId = Guid.Parse("6FC60999-8FC8-46B6-A131-897EDD45A5F0"),
                    RenterId = Guid.Parse("405004AA-8BD0-4407-A506-89E514C30FAF"),
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
                    AgentId = Guid.Parse("6FC60999-8FC8-46B6-A131-897EDD45A5F0"),
                };
                yachts.Add(yacht);

                return yachts.ToArray();
            }
        }
    }
