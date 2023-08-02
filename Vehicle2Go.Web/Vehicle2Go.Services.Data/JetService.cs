namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using Models.Vehicle;
    using Web.ViewModels.Vehicle;
    using Interfaces;
    using Vehicle2Go.Data;
    using Vehicle2Go.Data.Models.Vehicle;
    using Web.ViewModels.Vehicle.Enums;
    using Web.ViewModels.Agent;
    using Models.Statistics;
    using Vehicle2Go.Data.Models.Agent;

    public class JetService : IJetService
    {
        private readonly Vehicle2GoDbContext dbContext;

        public JetService(Vehicle2GoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> CreateAndReturnIdAsync(VehicleFormModel formModel, string agentId)
        {
            Jet newJet = new Jet()
            {
                Brand = formModel.Brand,
                Model = formModel.Model,
                RegistrationNumber = formModel.RegistrationNumber,
                Address = formModel.Address,
                PricePerDay = formModel.PricePerDay,
                Color = formModel.Color,
                ImageUrl = formModel.ImageUrl,
                CategoryId = formModel.CategoryId,
                AgentId = Guid.Parse(agentId),
            };

            await this.dbContext.Jets.AddAsync(newJet);
            await this.dbContext.SaveChangesAsync();

            return newJet.Id.ToString();
        }

        public async Task<AllVehiclesFilteredAndPagedServiceModel> AllAsync(AllVehiclesQueryModel queryModel)
        {
            IQueryable<Jet> jetQuery = this.dbContext
               .Jets
               .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                jetQuery = jetQuery
                    .Where(j => j.Category.Name == queryModel.Category);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";

                jetQuery = jetQuery
                    .Where
                    (j => EF.Functions.Like(j.Brand, wildCard) ||
                          EF.Functions.Like(j.Model, wildCard) ||
                          EF.Functions.Like(j.Address, wildCard) ||
                          EF.Functions.Like(j.RegistrationNumber, wildCard) ||
                          EF.Functions.Like(j.Color, wildCard));
            }

            jetQuery = queryModel.VehicleSorting switch
            {
                VehicleSorting.Newest => jetQuery
                    .OrderByDescending(j => j.CreatedOn),
                VehicleSorting.Oldest => jetQuery
                .OrderBy(j => j.CreatedOn),
                VehicleSorting.PriceAscending => jetQuery
                    .OrderBy(j => j.PricePerDay),
                VehicleSorting.PriceDescending => jetQuery
                    .OrderByDescending(j => j.PricePerDay),
                _ => jetQuery
                    .OrderBy(j => j.RenterId != null)
                    .ThenByDescending(j => j.CreatedOn),
            };

            IEnumerable<VehicleAllViewModel> allJets = await jetQuery
                .Where(j => j.IsActive)
                .Skip((queryModel.CurrentPage - 1) * queryModel.VehiclesPerPage)
                .Take(queryModel.VehiclesPerPage)
                .Select(j => new VehicleAllViewModel
                {
                    Id = j.Id.ToString(),
                    Brand = j.Brand,
                    Model = j.Model,
                    RegistrationNumber = j.RegistrationNumber,
                    Address = j.Address,
                    Color = j.Color,
                    ImageUrl = j.ImageUrl,
                    PricePerDay = j.PricePerDay,
                    IsRented = j.RenterId.HasValue
                })
                .ToArrayAsync();

            int totalJets = jetQuery.Count();

            return new AllVehiclesFilteredAndPagedServiceModel
            {
                TotalVehiclesCount = totalJets,
                Vehicles = allJets
            };
        }

        public async Task<IEnumerable<VehicleAllViewModel>> AllByAgentIdAsync(string agentId)
        {
            IEnumerable<VehicleAllViewModel> allAgentJets = await this.dbContext
                .Jets
                .Where(j => j.IsActive && j.AgentId.ToString() == agentId)
                .Select(j => new VehicleAllViewModel
                {
                    Id = j.Id.ToString(),
                    Brand = j.Brand,
                    Model = j.Model,
                    RegistrationNumber = j.RegistrationNumber,
                    Address = j.Address,
                    Color = j.Color,
                    ImageUrl = j.ImageUrl,
                    PricePerDay = j.PricePerDay,
                    IsRented = j.RenterId.HasValue
                })
                .ToArrayAsync();

            return allAgentJets;
        }

        public async Task<IEnumerable<VehicleAllViewModel>> AllByUserIdAsync(string userId)
        {
            IEnumerable<VehicleAllViewModel> allUserJets = await this.dbContext
                .Jets
                .Where(j => j.IsActive &&
                                j.RenterId.HasValue &&
                                j.RenterId.ToString() == userId)
                .Select(j => new VehicleAllViewModel
                {
                    Id = j.Id.ToString(),
                    Brand = j.Brand,
                    Model = j.Model,
                    RegistrationNumber = j.RegistrationNumber,
                    Address = j.Address,
                    Color = j.Color,
                    ImageUrl = j.ImageUrl,
                    PricePerDay = j.PricePerDay,
                    IsRented = j.RenterId.HasValue
                })
                .ToArrayAsync();

            return allUserJets;
        }

        public async Task<VehicleDetailsViewModel> GetDetailsByIdAsync(string jetId)
        {
            Jet jet = await this.dbContext
                .Jets
                .Include(j => j.Category)
                .Include(j => j.Agent)
                .ThenInclude(a => a.User)
                .Where(j => j.IsActive)
                .FirstAsync(j => j.Id.ToString() == jetId);

            return new VehicleDetailsViewModel
            {
                Id = jet.Id.ToString(),
                Brand = jet.Brand,
                Model = jet.Model,
                RegistrationNumber = jet.RegistrationNumber,
                Address = jet.Address,
                Color = jet.Color,
                ImageUrl = jet.ImageUrl,
                PricePerDay = jet.PricePerDay,
                IsRented = jet.RenterId.HasValue,
                Category = jet.Category.Name,
                Agent = new AgentInfoOnVehicleViewModel
                {
                    Email = jet.Agent.User.Email,
                    PhoneNumber = jet.Agent.PhoneNumber,
                    Address = jet.Agent.Address,
                }
            };
        }

        public async Task<bool> ExistByIdAsync(string jetId)
        {
            return await this.dbContext
                .Jets
                .Where(j => j.IsActive)
                .AnyAsync(j => j.Id.ToString() == jetId);
        }

        public async Task<VehicleFormModel> GetJetForEditByIdAsync(string jetId)
        {
            Jet jet = await this.dbContext
                .Jets
                .Include(j => j.Category)
                .Where(j => j.IsActive)
                .FirstAsync(j => j.Id.ToString() == jetId);

            return new VehicleFormModel
            {
                Brand = jet.Brand,
                Model = jet.Model,
                RegistrationNumber = jet.RegistrationNumber,
                Address = jet.Address,
                PricePerDay = jet.PricePerDay,
                ImageUrl = jet.ImageUrl,
                Color = jet.Color,
                CategoryId = jet.CategoryId
            };
        }

        public async Task<bool> IsAgentWithIdOwnerOfJetWithIdAsync(string jetId, string agentId)
        {
            Jet jet = await this.dbContext
                .Jets
                .Where(j => j.IsActive)
                .FirstAsync(j => j.Id.ToString() == jetId);

            return jet.AgentId.ToString() == agentId;
        }

        public async Task EditJetByIdAndFormModelAsync(string jetId, VehicleFormModel jetFormModel)
        {
            Jet jet = await this.dbContext
                .Jets
                .Where(j => j.IsActive)
                .FirstAsync(j => j.Id.ToString() == jetId);

            jet.Brand = jetFormModel.Brand;
            jet.Model = jetFormModel.Model;
            jet.RegistrationNumber = jetFormModel.RegistrationNumber;
            jet.Address = jetFormModel.Address;
            jet.PricePerDay = jetFormModel.PricePerDay;
            jet.ImageUrl = jetFormModel.ImageUrl;
            jet.Color = jetFormModel.Color;
            jet.CategoryId = jetFormModel.CategoryId;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<VehiclePreDeleteDetailsViewModel> GetJetForDeleteByIdAsync(string jetId)
        {
            Jet jet = await this.dbContext
                .Jets
                .Where(j => j.IsActive)
                .FirstAsync(j => j.Id.ToString() == jetId);

            return new VehiclePreDeleteDetailsViewModel
            {
                Brand = jet.Brand,
                Model = jet.Model,
                RegistrationNumber = jet.RegistrationNumber,
                Address = jet.Address,
                ImageUrl = jet.ImageUrl,
            };
        }

        public async Task DeleteByIdAsync(string jetId)
        {
            Jet jetToDelete = await this.dbContext
                .Jets
                .Where(j => j.IsActive)
                .FirstAsync(j => j.Id.ToString() == jetId);

            jetToDelete.IsActive = false;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsRentedByIdAsync(string jetId)
        {
            Jet jet = await this.dbContext
                .Jets
                .FirstAsync(c => c.Id.ToString() == jetId);

            return jet.RenterId.HasValue;
        }

        public async Task RentJetAsync(string jetId, string userId)
        {
            Jet jet = await this.dbContext
                .Jets
                .FirstAsync(c => c.Id.ToString() == jetId);

            jet.RenterId = Guid.Parse(userId);

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsRentedByUserWithIdAsync(string jetId, string userId)
        {
            Jet jet = await this.dbContext
                .Jets
                .FirstAsync(c => c.Id.ToString() == jetId);

            return jet.RenterId.HasValue &&
                   jet.RenterId.ToString() == userId;
        }

        public async Task LeaveAsync(string jetId)
        {
            Jet jet = await this.dbContext
                .Jets
                .FirstAsync(c => c.Id.ToString() == jetId);

            jet.RenterId = null;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<StatisticsServiceModel> GetStatisticsAsync()
        {
            return new StatisticsServiceModel
            {
                TotalVehicles = await this.dbContext.Jets.CountAsync(),
                TotalRents = await this.dbContext
                    .Jets
                    .Where(j => j.RenterId.HasValue)
                    .CountAsync()
            };
        }

        public async Task<bool> HasJetWithIdAsync(string userId, string jetId)
        {
            Agent? agent = await dbContext
                .Agents
                .FirstOrDefaultAsync(a => a.UserId.ToString() == userId);

            if (agent == null)
            {
                return false;
            }

            return agent.OwnedJets.Any(j => j.Id.ToString() == jetId);
        }
    }
}
