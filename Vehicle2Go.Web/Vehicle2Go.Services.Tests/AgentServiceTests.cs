namespace Vehicle2Go.Services.Tests
{
    using Microsoft.EntityFrameworkCore;

    using Web.ViewModels.Agent;
    using Vehicle2Go.Data;
    using Data;
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

        [Test]
        public async Task AgentExistByPhoneShouldReturnTrueWhenExist()
        {
            string existingAgentPhone = AgentUser.PhoneNumber;

            bool result = await agentService.AgentExistByPhoneNumberAsync(existingAgentPhone);

            Assert.True(result);
        }

        [Test]
        public async Task AgentExistByPhoneShouldReturnFalseWhenNotExist()
        {
            string existingAgentPhone = "+1111111111";

            bool result = await agentService.AgentExistByUserIdAsync(existingAgentPhone);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task CreateAgentShouldWorkCorrectly()
        {
            int agentsCountBefore = dbContext.Agents.Count();

            await agentService.Create(Agent.UserId.ToString(), new BecomeAgentFormModel()
            {
                PhoneNumber = Agent.PhoneNumber,
                Address = Agent.Address,
            });

            int agentsCountAfter = dbContext.Agents.Count();

            Assert.That(agentsCountAfter, Is.EqualTo(agentsCountBefore + 1));
        }

        [Test]
        public async Task GetAgentIdByUserIdShouldReturnTrueWhenExist()
        {
            var currentUserId = Agent.UserId.ToString();
            var currentAgentId = Agent.Id.ToString();

            var expectedAgentId = await agentService.GetAgentIdByUserIdAsync(currentUserId);

            Assert.That(expectedAgentId, Is.EqualTo(currentAgentId));
        }

        [Test]
        public async Task GetAgentIdByUserIdShouldReturnFalseWhenNotExist()
        {
            var currentUserId = Agent.Id.ToString();
            var currentAgentId = Agent.Id.ToString();

            var expectedAgentId = await agentService.GetAgentIdByUserIdAsync(currentUserId);

            Assert.AreNotEqual(expectedAgentId, currentAgentId);
        }

        [Test]
        public async Task HasVehicleWithIdAsync_ShouldReturnTrue_WhenAgentOwnVehicle()
        {
            var currentUserId = Agent.UserId.ToString();
            var vehicleId = Car.Id.ToString().ToLower();

            Agent.OwnedCars.Add(Car);

            var result = await agentService.HasVehicleWithIdAsync(currentUserId, vehicleId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task HasVehicleWithIdAsync_ShouldReturnFalse_WhenAgentDoesNotOwnVehicle()
        {
            var currentUserId = Agent.UserId.ToString();
            var vehicleId = "vehicle123";

            Agent.OwnedCars.Add(Car);

            var result = await agentService.HasVehicleWithIdAsync(currentUserId, vehicleId);

            Assert.IsFalse(result);
        }
    }
}