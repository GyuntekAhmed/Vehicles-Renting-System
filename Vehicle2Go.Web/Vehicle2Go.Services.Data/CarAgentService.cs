namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

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
    }
}
