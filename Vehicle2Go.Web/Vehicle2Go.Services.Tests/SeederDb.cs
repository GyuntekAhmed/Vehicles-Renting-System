// ReSharper disable MemberCanBePrivate.Global

namespace Vehicle2Go.Services.Tests
{
    using Microsoft.AspNetCore.Identity;

    using Vehicle2Go.Data;
    using Vehicle2Go.Data.Models.User;
    using Vehicle2Go.Data.Models.Agent;
    using Vehicle2Go.Data.Models.Vehicle;

    public static class SeederDb
    {
        public static ApplicationUser AgentUser;
        public static ApplicationUser RenterUser;
        public static Agent Agent;
        public static Car Car;

        public static void SeedDatabase(Vehicle2GoDbContext dbContext)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            AgentUser = new ApplicationUser()
            {
                UserName = "agent@abv.bg",
                NormalizedUserName = "AGENT@ABV.BG",
                Email = "agent@abv.bg",
                NormalizedEmail = "AGENT@ABV.BG",
                EmailConfirmed = true,
                PhoneNumber = "+359893794549",
                FirstName = "Stefan",
                LastName = "Stefanov",
                SecurityStamp = "4AAZPELNS27DV5ZVG7ZQO5GPW3Y3JSEA",
                ConcurrencyStamp = "4AAZPELNS27DV5ZVG7ZQO5GPW3Y3JSEC",
                TwoFactorEnabled = false
            };

            AgentUser.PasswordHash = hasher.HashPassword(AgentUser, "aaaaaa");

            RenterUser = new ApplicationUser()
            {
                UserName = "user@abv.bg",
                NormalizedUserName = "USER@ABV.BG",
                Email = "user@abv.bg",
                NormalizedEmail = "USER@ABV.BG",
                EmailConfirmed = true,
                PhoneNumber = "+359893794548",
                FirstName = "Gosho",
                LastName = "Goshov",
                SecurityStamp = "4AAZPELNS27DV5ZVG7ZQO5GPW3Y3JSEB",
                ConcurrencyStamp = "4AAZPELNS27DV5ZVG7ZQO5GPW3Y3JSED",
                TwoFactorEnabled = false
            };

            RenterUser.PasswordHash = hasher.HashPassword(RenterUser, "aaaaaa");

            Agent = new Agent
            {
                PhoneNumber = "+359893794549",
                Address = "Silistra",
                User = AgentUser,
            };

            Car = new Car
            {
                Brand = "Hyundai",
                Model = "Santa fe",
                RegistrationNumber = "CC1835AK",
                Address = "Silistra, Center",
                PricePerDay = 80,
                Color = "White",
                ImageUrl =
                    "https://upload.wikimedia.org/wikipedia/commons/f/fb/2010_Hyundai_Santa_Fe_GLS_--_09-24-2010.jpg",
                CategoryId = 1,
                AgentId = Guid.Parse("8af114de-010e-4b30-8920-9064facf5ae1"),
                RenterId = RenterUser.Id,
                CreatedOn = DateTime.UtcNow,
                IsActive = true
            };

            dbContext.Cars.Add(Car);
            dbContext.Users.Add(AgentUser);
            dbContext.Users.Add(RenterUser);
            dbContext.Agents.Add(Agent);

            dbContext.SaveChanges();
        }
    }
}
