namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    using Vehicle2Go.Data.Models.Agent;
    using Web.ViewModels.Agent;
    using Vehicle2Go.Data;
    using Interfaces;

    public class AgentService : IAgentService
    {
        private readonly Vehicle2GoDbContext dbContext;

        public AgentService(Vehicle2GoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AgentExistByUserIdAsync(string userId)
        {
            return await this.dbContext
                .Agents
                .AnyAsync(a => a.UserId.ToString() == userId);
        }

        public async Task<bool> AgentExistByPhoneNumberAsync(string phoneNumber)
        {
            return await this.dbContext
                .Agents
                .AnyAsync(a => a.PhoneNumber == phoneNumber);
        }

        public async Task Create(string userId, BecomeAgentFormModel model)
        {
            Agent newAgent = new Agent()
            {
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                UserId = Guid.Parse(userId),
            };

            await this.dbContext.Agents.AddAsync(newAgent);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<string?> GetAgentIdByUserIdAsync(string userId)
        {
            Agent? agent = await this.dbContext
                .Agents
                .FirstOrDefaultAsync(a => a.UserId.ToString() == userId);

            if (agent == null)
            {
                return null;
            }

            return agent.Id.ToString();
        }
    }
}
