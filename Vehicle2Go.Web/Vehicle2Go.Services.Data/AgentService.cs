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

        public async Task<bool> HasVehicleWithIdAsync(string? userId, string vehicleId)
        {
            Agent? agent = await dbContext
                .Agents
                .Include(a => a.OwnedCars)
                .Include(a => a.OwnedMotorcycles)
                .Include(a => a.OwnedJets)
                .Include(a => a.OwnedYachts)
                .Include(a => a.OwnedTrucks)
                .FirstOrDefaultAsync(a => a.UserId.ToString() == userId);

            vehicleId = vehicleId.ToLower();

            if (agent == null)
            {
                return false;
            }

            bool hasCar = agent.OwnedCars.Any();
            bool hasMotorcycle = agent.OwnedMotorcycles.Any();
            bool hasJet = agent.OwnedJets.Any();
            bool hasYacht = agent.OwnedYachts.Any();
            bool hasTruck = agent.OwnedTrucks.Any();

            if (hasCar && agent.OwnedCars.Any(c => c.Id.ToString() == vehicleId))
            {
                return agent.OwnedCars.Any(c => c.Id.ToString() == vehicleId);
            }
            else if (hasMotorcycle && agent.OwnedMotorcycles.Any(m => m.Id.ToString() == vehicleId))
            {
                return agent.OwnedMotorcycles.Any(m => m.Id.ToString() == vehicleId);
            }
            else if (hasJet && agent.OwnedJets.Any(j => j.Id.ToString() == vehicleId))
            {
                return agent.OwnedJets.Any(j => j.Id.ToString() == vehicleId);
            }
            else if (hasYacht && agent.OwnedYachts.Any(y => y.Id.ToString() == vehicleId))
            {
                return agent.OwnedYachts.Any(y => y.Id.ToString() == vehicleId);
            }
            else if (hasTruck && agent.OwnedTrucks.Any(t => t.Id.ToString() == vehicleId))
            {
                return agent.OwnedTrucks.Any(t => t.Id.ToString() == vehicleId);
            }
            else
            {
                return false;
            }
        }
    }
}
