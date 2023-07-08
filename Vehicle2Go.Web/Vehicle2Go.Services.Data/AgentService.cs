namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using Vehicle2Go.Data.Models;
    using Web.ViewModels.Agent;
    using Vehicle2Go.Data;
    using Interfaces;

    public class AgentService : IAgentService
    {
        private readonly VehicleDbContext dbContext;

        public AgentService(VehicleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AgentExistByUserIdAsync(string userId)
        {
            bool result = await this.dbContext
                .Agents
                .AnyAsync(a => a.UserId.ToString() == userId);

            return result;
        }

        public async Task<bool> AgentExistByPhoneNumberAsync(string phoneNumber)
        {
            bool result = await this.dbContext
                .Agents
                .AnyAsync(a => a.PhoneNumber == phoneNumber);

            return result;
        }

        public async Task<bool> HasRentsByUserIdAsync(string userId)
        {
            var user = await this.dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            if (user == null)
            {
                return false;
            }

            return user.RentedCars.Any();
        }

        public async Task Create(string userId, BecomeAgentFormModel model)
        {
            Agent newAgent = new Agent
            {
                UserId = Guid.Parse(userId),
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
            };

            await dbContext.Agents.AddAsync(newAgent);
            await dbContext.SaveChangesAsync();
        }
    }
}
