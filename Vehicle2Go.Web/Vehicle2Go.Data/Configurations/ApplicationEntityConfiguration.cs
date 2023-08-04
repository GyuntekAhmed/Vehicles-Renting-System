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
            // Uncomment before Seeding DB
            //builder.HasData(this.GenerateApplicationUser());
        }

        private ApplicationUser GenerateApplicationUser()
        {
            ApplicationUser user = new ApplicationUser
            {
                Id = Guid.Parse("389A42BB-6250-4E01-A7C6-29632BF524FA"),
                UserName = "gyuntekahmed@abv.bg",
                NormalizedUserName = "GYUNTEKAHMED@ABV.BG",
                Email = "gyuntekahmed@abv.bg",
                NormalizedEmail = "GYUNTEKAHMED@ABV.BG",
                PasswordHash = "AQAAAAEAACcQAAAAEK9pUrX4QwPX9IBiaBysvKvCrXeM36jvfu3Z+m78TRtLJ1ZhnTrZvedK/a12Zhlppw==",
                SecurityStamp = "4AAZPELNS27DV5ZVG7ZQO5GPW3Y3JSEA",
                ConcurrencyStamp = "9d36e3b1-b876-4b86-b708-d757166579ad",
                PhoneNumber = null,
                FirstName = "Test",
                LastName = "Testov",
            };

            return user;
        }
    }
}
