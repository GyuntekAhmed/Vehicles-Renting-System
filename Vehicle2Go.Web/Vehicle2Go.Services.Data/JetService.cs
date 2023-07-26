﻿namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using Models.Vehicle;
    using Web.ViewModels.Vehicle;
    using Interfaces;
    using Vehicle2Go.Data;
    using Vehicle2Go.Data.Models.Vehicle;
    using Web.ViewModels.Vehicle.Enums;
    using Web.ViewModels.Agent;

    public class JetService : IJetService
    {
        private readonly Vehicle2GoDbContext dbContext;

        public JetService(Vehicle2GoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(VehicleFormModel formModel, string agentId)
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

        public async Task<VehicleDetailsViewModel?> GetDetailsByIdAsync(string jetId)
        {
            Jet? jet = await this.dbContext
                .Jets
                .Include(j => j.Category)
                .Include(j => j.Agent)
                .ThenInclude(a => a.User)
                .Where(j => j.IsActive)
                .FirstOrDefaultAsync(j => j.Id.ToString() == jetId);

            if (jet == null)
            {
                return null;
            }

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
    }
}
