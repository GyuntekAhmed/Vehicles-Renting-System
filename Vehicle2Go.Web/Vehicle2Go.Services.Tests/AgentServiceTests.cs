using Vehicle2Go.Services.Data;

namespace Vehicle2Go.Services.Tests
{
    using Microsoft.EntityFrameworkCore;

    using Vehicle2Go.Data;
    using Data.Interfaces;

    using static SeederDb;

    public class AgentServiceTests
    {
        private DbContextOptions<Vehicle2GoDbContext> dbOptions;
        private Vehicle2GoDbContext dbContext;
        private IAgentService agentService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            this.dbOptions = new DbContextOptionsBuilder<Vehicle2GoDbContext>()
                .UseInMemoryDatabase
                    ("Vehicle2GoInMemory" + Guid.NewGuid().ToString()).Options;

            this.dbContext = new Vehicle2GoDbContext(dbOptions);
            this.dbContext.Database.EnsureCreated();

            SeedDatabase(this.dbContext);

            this.agentService = new AgentService(this.dbContext);
        }

        [Test]
        public async Task AgentExistByUserIdShouldReturnTrueWhenExist()
        {
            string existingAgentUserId = AgentUser.Id.ToString();

            bool result = await agentService.AgentExistByUserIdAsync(existingAgentUserId);

            Assert.True(result);
        }

        [Test]
        public async Task AgentExistByUserIdShouldReturnFalseWhenNotExist()
        {
            string existingAgentUserId = RenterUser.Id.ToString();

            bool result = await agentService.AgentExistByUserIdAsync(existingAgentUserId);

            Assert.IsFalse(result);
        }
    }
}