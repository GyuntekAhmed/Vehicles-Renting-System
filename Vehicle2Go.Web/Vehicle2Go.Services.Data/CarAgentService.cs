using Vehicle2Go.Data.Models.Agent;

namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    using Web.ViewModels.Agent;
    using Vehicle2Go.Data;
    using Interfaces;

    public class CarAgentService : ICarAgentService
    {
        private readonly Vehicle2GoDbContext dbContext;

        public CarAgentService(Vehicle2GoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AgentExistByUserIdAsync(string userId)
        {
            return await this.dbContext
                .CarAgents
                .AnyAsync(a => a.UserId.ToString() == userId);
        }

        public async Task<bool> AgentExistByPhoneNumberAsync(string phoneNumber)
        {
            return await this.dbContext
                .CarAgents
                .AnyAsync(a => a.PhoneNumber == phoneNumber);
        }

        public async Task Create(string userId, BecomeAgentFormModel model)
        {
            CarAgent newAgent = new CarAgent()
            {
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                UserId = Guid.Parse(userId)
            };

            await this.dbContext.CarAgents.AddAsync(newAgent);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<string?> GetAgentIdByUserIdAsync(string userId)
        {
            CarAgent? agent = await this.dbContext
                .CarAgents
                .FirstOrDefaultAsync(a => a.UserId.ToString() == userId);

            if (agent == null)
            {
                return null;
            }

            return agent.Id.ToString();
        }
    }
}
