using Microsoft.AspNetCore.Identity;

namespace Vehicle2Go.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models.User;

    public class ApplicationEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FirstName)
                .HasDefaultValue("Test");

            builder.Property(u => u.LastName)
                .HasDefaultValue("Testov");

            builder.HasData(this.GenerateApplicationUser());
        }

        private ApplicationUser GenerateApplicationUser()
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            ApplicationUser user = new ApplicationUser
            {
                Id = Guid.Parse("e18cd243-7762-4aab-baee-8c8977e6ac83"),
                UserName = "agent@abv.bg",
                NormalizedUserName = "AGENT@ABV.BG",
                Email = "agent@abv.bg",
                NormalizedEmail = "AGENT@ABV.BG",
                PhoneNumber = "+359893794549",
                FirstName = "Test",
                LastName = "Testov",
                SecurityStamp = "4AAZPELNS27DV5ZVG7ZQO5GPW3Y3JSEA",
            };
            user.PasswordHash = hasher.HashPassword(user, "aaaaaa");

            return user;
        }
    }
}
