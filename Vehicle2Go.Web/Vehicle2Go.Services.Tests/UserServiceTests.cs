using Vehicle2Go.Data.Models.User;
using Vehicle2Go.Web.ViewModels.User;

namespace Vehicle2Go.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    
    using Vehicle2Go.Data;
    using Data;
    using Data.Interfaces;

    using static SeederDb;

    public class UserServiceTests
    {
        private DbContextOptions<Vehicle2GoDbContext> dbOptions;
        private Vehicle2GoDbContext dbContext;
        private IUserService userService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            this.dbOptions = new DbContextOptionsBuilder<Vehicle2GoDbContext>()
                .UseInMemoryDatabase
                    ("Vehicle2GoInMemory" + Guid.NewGuid().ToString()).Options;

            this.dbContext = new Vehicle2GoDbContext(dbOptions);
            this.dbContext.Database.EnsureCreated();

            SeedDatabase(this.dbContext);

            this.userService = new UserService(this.dbContext);
        }

        [Test]
        public async Task GetFullNameAsync_ShouldReturnTrueWhenUserExist()
        {
            string currentFullName = RenterUser.FirstName + " " + RenterUser.LastName;

            string expectedFullName = await userService.GetFullNameAsync(RenterUser.Email);

            Assert.That(currentFullName, Is.EqualTo(expectedFullName));
        }

        [Test]
        public async Task GetFullNameAsync_ShouldReturnFalseWhenUserNotExist()
        {
            string currentFullName = "Pesho Petrov";

            string expectedFullName = await userService.GetFullNameAsync(RenterUser.Email);

            Assert.AreNotEqual(expectedFullName, currentFullName);
        }

        [Test]
        public async Task AllUsersAsync_ShouldWorkCorrectly()
        {
            var users = new List<UserViewModel>
            {
                new UserViewModel
                {
                    Id = RenterUser.Id.ToString(),
                    Email = RenterUser.Email,
                    PhoneNumber = RenterUser.PhoneNumber,
                    FullName = RenterUser.FirstName + " " + RenterUser.LastName,
                },
                new UserViewModel
                {
                    Id = AgentUser.Id.ToString(),
                    Email = AgentUser.Email,
                    PhoneNumber = AgentUser.PhoneNumber,
                    FullName = AgentUser.FirstName + " " + AgentUser.LastName,
                },
                new UserViewModel
                {
                    Id = Agent.Id.ToString(),
                    Email = Agent.User.Email,
                    PhoneNumber = Agent.PhoneNumber,
                    FullName = Agent.User.FirstName + " " + Agent.User.LastName,
                }
            };

            var result = await userService.AllUsersAsync();

            Assert.That(result.Count(), Is.EqualTo(users.Count));
        }
    }
}
