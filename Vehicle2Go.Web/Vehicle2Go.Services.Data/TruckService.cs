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

    public class TruckService : ITruckService
    {
        private readonly Vehicle2GoDbContext dbContext;

        public TruckService(Vehicle2GoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(VehicleFormModel formModel, string agentId)
        {
            Truck newTruck = new Truck()
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

            await this.dbContext.Trucks.AddAsync(newTruck);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<AllVehiclesFilteredAndPagedServiceModel> AllAsync(AllVehiclesQueryModel queryModel)
        {
            IQueryable<Truck> truckQuery = this.dbContext
               .Trucks
               .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                truckQuery = truckQuery
                    .Where(t => t.Category.Name == queryModel.Category);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";

                truckQuery = truckQuery
                    .Where
                    (t => EF.Functions.Like(t.Brand, wildCard) ||
                          EF.Functions.Like(t.Model, wildCard) ||
                          EF.Functions.Like(t.Address, wildCard) ||
                          EF.Functions.Like(t.RegistrationNumber, wildCard) ||
                          EF.Functions.Like(t.Color, wildCard));
            }

            truckQuery = queryModel.VehicleSorting switch
            {
                VehicleSorting.Newest => truckQuery
                    .OrderByDescending(t => t.CreatedOn),
                VehicleSorting.Oldest => truckQuery
                .OrderBy(t => t.CreatedOn),
                VehicleSorting.PriceAscending => truckQuery
                    .OrderBy(t => t.PricePerDay),
                VehicleSorting.PriceDescending => truckQuery
                    .OrderByDescending(t => t.PricePerDay),
                _ => truckQuery
                    .OrderBy(t => t.RenterId != null)
                    .ThenByDescending(t => t.CreatedOn),
            };

            IEnumerable<VehicleAllViewModel> allTrucks = await truckQuery
                .Where(t => t.IsActive)
                .Skip((queryModel.CurrentPage - 1) * queryModel.VehiclesPerPage)
                .Take(queryModel.VehiclesPerPage)
                .Select(t => new VehicleAllViewModel
                {
                    Id = t.Id.ToString(),
                    Brand = t.Brand,
                    Model = t.Model,
                    RegistrationNumber = t.RegistrationNumber,
                    Address = t.Address,
                    Color = t.Color,
                    ImageUrl = t.ImageUrl,
                    PricePerDay = t.PricePerDay,
                    IsRented = t.RenterId.HasValue
                })
                .ToArrayAsync();

            int totalTrucks = truckQuery.Count();

            return new AllVehiclesFilteredAndPagedServiceModel
            {
                TotalVehiclesCount = totalTrucks,
                Vehicles = allTrucks
            };
        }

        public async Task<IEnumerable<VehicleAllViewModel>> AllByAgentIdAsync(string agentId)
        {
            IEnumerable<VehicleAllViewModel> allAgentTrucks = await this.dbContext
                .Trucks
                .Where(t => t.IsActive && t.AgentId.ToString() == agentId)
                .Select(t => new VehicleAllViewModel
                {
                    Id = t.Id.ToString(),
                    Brand = t.Brand,
                    Model = t.Model,
                    RegistrationNumber = t.RegistrationNumber,
                    Address = t.Address,
                    Color = t.Color,
                    ImageUrl = t.ImageUrl,
                    PricePerDay = t.PricePerDay,
                    IsRented = t.RenterId.HasValue
                })
                .ToArrayAsync();

            return allAgentTrucks;
        }

        public async Task<IEnumerable<VehicleAllViewModel>> AllByUserIdAsync(string userId)
        {
            IEnumerable<VehicleAllViewModel> allUserTrucks = await this.dbContext
                .Trucks
                .Where(t => t.IsActive &&
                                t.RenterId.HasValue &&
                                t.RenterId.ToString() == userId)
                .Select(t => new VehicleAllViewModel
                {
                    Id = t.Id.ToString(),
                    Brand = t.Brand,
                    Model = t.Model,
                    RegistrationNumber = t.RegistrationNumber,
                    Address = t.Address,
                    Color = t.Color,
                    ImageUrl = t.ImageUrl,
                    PricePerDay = t.PricePerDay,
                    IsRented = t.RenterId.HasValue
                })
                .ToArrayAsync();

            return allUserTrucks;
        }

        public async Task<VehicleDetailsViewModel> GetDetailsByIdAsync(string truckId)
        {
            Truck truck = await this.dbContext
                .Trucks
                .Include(t => t.Category)
                .Include(t => t.Agent)
                .ThenInclude(a => a.User)
                .Where(t => t.IsActive)
                .FirstAsync(t => t.Id.ToString() == truckId);
            
            return new VehicleDetailsViewModel
            {
                Id = truck.Id.ToString(),
                Brand = truck.Brand,
                Model = truck.Model,
                RegistrationNumber = truck.RegistrationNumber,
                Address = truck.Address,
                Color = truck.Color,
                ImageUrl = truck.ImageUrl,
                PricePerDay = truck.PricePerDay,
                IsRented = truck.RenterId.HasValue,
                Category = truck.Category.Name,
                Agent = new AgentInfoOnVehicleViewModel
                {
                    Email = truck.Agent.User.Email,
                    PhoneNumber = truck.Agent.PhoneNumber,
                    Address = truck.Agent.Address,
                }
            };
        }

        public async Task<bool> ExistByIdAsync(string truckId)
        {
            return await this.dbContext
                .Trucks
                .Where(t => t.IsActive)
                .AnyAsync(t => t.Id.ToString() == truckId);
        }

        public async Task<VehicleFormModel> GetTruckForEditByIdAsync(string truckId)
        {
            Truck truck = await this.dbContext
                .Trucks
                .Include(t => t.Category)
                .Where(t => t.IsActive)
                .FirstAsync(t => t.Id.ToString() == truckId);

            return new VehicleFormModel
            {
                Brand = truck.Brand,
                Model = truck.Model,
                RegistrationNumber = truck.RegistrationNumber,
                Address = truck.Address,
                PricePerDay = truck.PricePerDay,
                ImageUrl = truck.ImageUrl,
                Color = truck.Color,
                CategoryId = truck.CategoryId
            };
        }

        public async Task<bool> IsAgentWithIdOwnerOfTruckWithIdAsync(string truckId, string agentId)
        {
            Truck truck = await this.dbContext
                .Trucks
                .Where(t => t.IsActive)
                .FirstAsync(t => t.Id.ToString() == truckId);

            return truck.AgentId.ToString() == agentId;
        }

        public async Task EditTruckByIdAndFormModelAsync(string truckId, VehicleFormModel truckFormModel)
        {
            Truck truck = await this.dbContext
                .Trucks
                .Where(t => t.IsActive)
                .FirstAsync(t => t.Id.ToString() == truckId);

            truck.Brand = truckFormModel.Brand;
            truck.Model = truckFormModel.Model;
            truck.RegistrationNumber = truckFormModel.RegistrationNumber;
            truck.Address = truckFormModel.Address;
            truck.PricePerDay = truckFormModel.PricePerDay;
            truck.ImageUrl = truckFormModel.ImageUrl;
            truck.Color = truckFormModel.Color;
            truck.CategoryId = truckFormModel.CategoryId;

            await this.dbContext.SaveChangesAsync();
        }
    }
}
