namespace VehiclesRenting.Services
{
    using Microsoft.EntityFrameworkCore;

    using Data.Models;
    using Data;
    using Interfaces;
    using Web.ViewModels.Agent;

    public class AgentService : IAgentService
    {
        private readonly VehiclesRentingDbContext dbContext;

        public AgentService(VehiclesRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AgentExistByIdAsync(string agentId)
        {
            bool result = await this.dbContext
                .Agents
                .AnyAsync(a => a.UserId.ToString() == agentId);

            return result;
        }

        public async Task<bool> AgentExistsByPhoneAsync(string phoneNumber)
        {
            bool result = await this.dbContext
                .Agents
                .AnyAsync(a => a.PhoneNumber == phoneNumber);

            return result;
        }

        public async Task Create(string userId, BecomeAgentFormModel model)
        {
            Agent agent = new Agent()
            {
                Id = Guid.Parse(userId),
                PhoneNumber = model.PhoneNumber,
            };

            await this.dbContext.Agents.AddAsync(agent);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> UserHasRentsAsync(string userId)
        {

            var user = await dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (user == null)
            {
                return false;
            }

            if (user.RentedCars.Any())
            {
                return user.RentedCars.Any();
            }

            if (user.RentedMotorcycles.Any())
            {
                return user.RentedMotorcycles.Any();
            }

            if (user.RentedScooters.Any())
            {
                return user.RentedScooters.Any();
            }

            return false;
        }
    }
}
