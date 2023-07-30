namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using Interfaces;
    using Web.ViewModels.Vehicle;
    using Vehicle2Go.Data.Models.Vehicle;
    using Vehicle2Go.Data;
    using Web.ViewModels.Vehicle.Enums;
    using Models.Vehicle;
    using Web.ViewModels.Agent;

    public class MotorcycleService : IMotorcycleService
    {
        private readonly Vehicle2GoDbContext dbContext;

        public MotorcycleService(Vehicle2GoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> CreateAndReturnIdAsync(VehicleFormModel formModel, string agentId)
        {
            Motorcycle newMotorcycle = new Motorcycle()
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

            await this.dbContext.Motorcycles.AddAsync(newMotorcycle);
            await this.dbContext.SaveChangesAsync();

            return newMotorcycle.Id.ToString();
        }

        public async Task<AllVehiclesFilteredAndPagedServiceModel> AllAsync(AllVehiclesQueryModel queryModel)
        {
            IQueryable<Motorcycle> motorcycleQuery = this.dbContext
               .Motorcycles
               .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                motorcycleQuery = motorcycleQuery
                    .Where(m => m.Category.Name == queryModel.Category);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";

                motorcycleQuery = motorcycleQuery
                    .Where
                    (m => EF.Functions.Like(m.Brand, wildCard) ||
                          EF.Functions.Like(m.Model, wildCard) ||
                          EF.Functions.Like(m.Address, wildCard) ||
                          EF.Functions.Like(m.RegistrationNumber, wildCard) ||
                          EF.Functions.Like(m.Color, wildCard));
            }

            motorcycleQuery = queryModel.VehicleSorting switch
            {
                VehicleSorting.Newest => motorcycleQuery
                    .OrderByDescending(m => m.CreatedOn),
                VehicleSorting.Oldest => motorcycleQuery
                .OrderBy(m => m.CreatedOn),
                VehicleSorting.PriceAscending => motorcycleQuery
                    .OrderBy(m => m.PricePerDay),
                VehicleSorting.PriceDescending => motorcycleQuery
                    .OrderByDescending(m => m.PricePerDay),
                _ => motorcycleQuery
                    .OrderBy(m => m.RenterId != null)
                    .ThenByDescending(m => m.CreatedOn),
            };

            IEnumerable<VehicleAllViewModel> allMotorcycles = await motorcycleQuery
                .Where(m => m.IsActive)
                .Skip((queryModel.CurrentPage - 1) * queryModel.VehiclesPerPage)
                .Take(queryModel.VehiclesPerPage)
                .Select(m => new VehicleAllViewModel
                {
                    Id = m.Id.ToString(),
                    Brand = m.Brand,
                    Model = m.Model,
                    RegistrationNumber = m.RegistrationNumber,
                    Address = m.Address,
                    Color = m.Color,
                    ImageUrl = m.ImageUrl,
                    PricePerDay = m.PricePerDay,
                    IsRented = m.RenterId.HasValue
                })
                .ToArrayAsync();

            int totalMotorcycles = motorcycleQuery.Count();

            return new AllVehiclesFilteredAndPagedServiceModel
            {
                TotalVehiclesCount = totalMotorcycles,
                Vehicles = allMotorcycles
            };
        }

        public async Task<IEnumerable<VehicleAllViewModel>> AllByAgentIdAsync(string agentId)
        {
            IEnumerable<VehicleAllViewModel> allAgentMotorcycles = await this.dbContext
                .Motorcycles
                .Where(m => m.IsActive && m.AgentId.ToString() == agentId)
                .Select(m => new VehicleAllViewModel
                {
                    Id = m.Id.ToString(),
                    Brand = m.Brand,
                    Model = m.Model,
                    RegistrationNumber = m.RegistrationNumber,
                    Address = m.Address,
                    Color = m.Color,
                    ImageUrl = m.ImageUrl,
                    PricePerDay = m.PricePerDay,
                    IsRented = m.RenterId.HasValue
                })
                .ToArrayAsync();

            return allAgentMotorcycles;
        }

        public async Task<IEnumerable<VehicleAllViewModel>> AllByUserIdAsync(string userId)
        {
            IEnumerable<VehicleAllViewModel> allUserMotorcycles = await this.dbContext
                .Motorcycles
                .Where(m => m.IsActive &&
                                    m.RenterId.HasValue &&
                                    m.RenterId.ToString() == userId)
                .Select(m => new VehicleAllViewModel
                {
                    Id = m.Id.ToString(),
                    Brand = m.Brand,
                    Model = m.Model,
                    RegistrationNumber = m.RegistrationNumber,
                    Address = m.Address,
                    Color = m.Color,
                    ImageUrl = m.ImageUrl,
                    PricePerDay = m.PricePerDay,
                    IsRented = m.RenterId.HasValue
                })
                .ToArrayAsync();

            return allUserMotorcycles;
        }

        public async Task<VehicleDetailsViewModel> GetDetailsByIdAsync(string motorcycleId)
        {
            Motorcycle motorcycle = await this.dbContext
                .Motorcycles
                .Include(m => m.Category)
                .Include(m => m.Agent)
                .ThenInclude(a => a.User)
                .Where(m => m.IsActive)
                .FirstAsync(m => m.Id.ToString() == motorcycleId);

            return new VehicleDetailsViewModel
            {
                Id = motorcycle.Id.ToString(),
                Brand = motorcycle.Brand,
                Model = motorcycle.Model,
                RegistrationNumber = motorcycle.RegistrationNumber,
                Address = motorcycle.Address,
                Color = motorcycle.Color,
                ImageUrl = motorcycle.ImageUrl,
                PricePerDay = motorcycle.PricePerDay,
                IsRented = motorcycle.RenterId.HasValue,
                Category = motorcycle.Category.Name,
                Agent = new AgentInfoOnVehicleViewModel
                {
                    Email = motorcycle.Agent.User.Email,
                    PhoneNumber = motorcycle.Agent.PhoneNumber,
                    Address = motorcycle.Agent.Address,
                }
            };
        }

        public async Task<bool> ExistByIdAsync(string motorcycleId)
        {
            return await this.dbContext
                .Motorcycles
                .Where(m => m.IsActive)
                .AnyAsync(m => m.Id.ToString() == motorcycleId);
        }

        public async Task<VehicleFormModel> GetMotorcycleForEditByIdAsync(string motorcycleId)
        {
            Motorcycle motorcycle = await this.dbContext
                .Motorcycles
                .Include(m => m.Category)
                .Where(m => m.IsActive)
                .FirstAsync(m => m.Id.ToString() == motorcycleId);

            return new VehicleFormModel
            {
                Brand = motorcycle.Brand,
                Model = motorcycle.Model,
                RegistrationNumber = motorcycle.RegistrationNumber,
                Address = motorcycle.Address,
                PricePerDay = motorcycle.PricePerDay,
                ImageUrl = motorcycle.ImageUrl,
                Color = motorcycle.Color,
                CategoryId = motorcycle.CategoryId
            };
        }

        public async Task<bool> IsAgentWithIdOwnerOfMotorcycleWithIdAsync(string motorcycleId, string agentId)
        {
            Motorcycle motorcycle = await this.dbContext
                .Motorcycles
                .Where(m => m.IsActive)
                .FirstAsync(m => m.Id.ToString() == motorcycleId);

            return motorcycle.AgentId.ToString() == agentId;
        }

        public async Task EditMotorcycleByIdAndFormModelAsync(string motorcycleId, VehicleFormModel motorcycleFormModel)
        {
            Motorcycle motorcycle = await this.dbContext
                .Motorcycles
                .Where(m => m.IsActive)
                .FirstAsync(m => m.Id.ToString() == motorcycleId);

            motorcycle.Brand = motorcycleFormModel.Brand;
            motorcycle.Model = motorcycleFormModel.Model;
            motorcycle.RegistrationNumber = motorcycleFormModel.RegistrationNumber;
            motorcycle.Address = motorcycleFormModel.Address;
            motorcycle.PricePerDay = motorcycleFormModel.PricePerDay;
            motorcycle.ImageUrl = motorcycleFormModel.ImageUrl;
            motorcycle.Color = motorcycleFormModel.Color;
            motorcycle.CategoryId = motorcycleFormModel.CategoryId;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<VehiclePreDeleteDetailsViewModel> GetMotorcycleForDeleteByIdAsync(string motorcycleId)
        {
            Motorcycle motorcycle = await this.dbContext
                .Motorcycles
                .Where(m => m.IsActive)
                .FirstAsync(m => m.Id.ToString() == motorcycleId);

            return new VehiclePreDeleteDetailsViewModel
            {
                Brand = motorcycle.Brand,
                Model = motorcycle.Model,
                RegistrationNumber = motorcycle.RegistrationNumber,
                Address = motorcycle.Address,
                ImageUrl = motorcycle.ImageUrl,
            };
        }

        public async Task DeleteByIdAsync(string motorcycleId)
        {
            Motorcycle motorcycleToDelete = await this.dbContext
                .Motorcycles
                .Where(m => m.IsActive)
                .FirstAsync(m => m.Id.ToString() == motorcycleId);

            motorcycleToDelete.IsActive = false;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsRentedByIdAsync(string motorcycleId)
        {
            Motorcycle motorcycle = await this.dbContext
                .Motorcycles
                .FirstAsync(c => c.Id.ToString() == motorcycleId);

            return motorcycle.RenterId.HasValue;
        }

        public async Task RentMotorcycleAsync(string motorcycleId, string userId)
        {
            Motorcycle motorcycle = await this.dbContext
                .Motorcycles
                .FirstAsync(c => c.Id.ToString() == motorcycleId);

            motorcycle.RenterId = Guid.Parse(userId);

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsRentedByUserWithIdAsync(string motorcycleId, string userId)
        {

            Motorcycle motorcycle = await this.dbContext
                .Motorcycles
                .FirstAsync(c => c.Id.ToString() == motorcycleId);

            return motorcycle.RenterId.HasValue &&
                   motorcycle.RenterId.ToString() == userId;
        }

        public async Task LeaveAsync(string motorcycleId)
        {

            Motorcycle motorcycle = await this.dbContext
                .Motorcycles
                .FirstAsync(c => c.Id.ToString() == motorcycleId);

            motorcycle.RenterId = null;

            await this.dbContext.SaveChangesAsync();
        }
    }
}
