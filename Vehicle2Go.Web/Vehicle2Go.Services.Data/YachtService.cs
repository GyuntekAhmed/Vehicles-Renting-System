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

    public class YachtService : IYachtService
    {
        private readonly Vehicle2GoDbContext dbContext;

        public YachtService(Vehicle2GoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(VehicleFormModel formModel, string agentId)
        {
            Yacht newYacht = new Yacht()
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

            await this.dbContext.Yachts.AddAsync(newYacht);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<AllVehiclesFilteredAndPagedServiceModel> AllAsync(AllVehiclesQueryModel queryModel)
        {
            IQueryable<Yacht> yachtQuery = this.dbContext
               .Yachts
               .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                yachtQuery = yachtQuery
                    .Where(y => y.Category.Name == queryModel.Category);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";

                yachtQuery = yachtQuery
                    .Where
                    (y => EF.Functions.Like(y.Brand, wildCard) ||
                          EF.Functions.Like(y.Model, wildCard) ||
                          EF.Functions.Like(y.Address, wildCard) ||
                          EF.Functions.Like(y.RegistrationNumber, wildCard) ||
                          EF.Functions.Like(y.Color, wildCard));
            }

            yachtQuery = queryModel.VehicleSorting switch
            {
                VehicleSorting.Newest => yachtQuery
                    .OrderByDescending(y => y.CreatedOn),
                VehicleSorting.Oldest => yachtQuery
                .OrderBy(y => y.CreatedOn),
                VehicleSorting.PriceAscending => yachtQuery
                    .OrderBy(y => y.PricePerDay),
                VehicleSorting.PriceDescending => yachtQuery
                    .OrderByDescending(y => y.PricePerDay),
                _ => yachtQuery
                    .OrderBy(y => y.RenterId != null)
                    .ThenByDescending(y => y.CreatedOn),
            };

            IEnumerable<VehicleAllViewModel> allYachts = await yachtQuery
                .Where(y => y.IsActive)
                .Skip((queryModel.CurrentPage - 1) * queryModel.VehiclesPerPage)
                .Take(queryModel.VehiclesPerPage)
                .Select(y => new VehicleAllViewModel
                {
                    Id = y.Id.ToString(),
                    Brand = y.Brand,
                    Model = y.Model,
                    RegistrationNumber = y.RegistrationNumber,
                    Address = y.Address,
                    Color = y.Color,
                    ImageUrl = y.ImageUrl,
                    PricePerDay = y.PricePerDay,
                    IsRented = y.RenterId.HasValue
                })
                .ToArrayAsync();

            int totalYachts = yachtQuery.Count();

            return new AllVehiclesFilteredAndPagedServiceModel
            {
                TotalVehiclesCount = totalYachts,
                Vehicles = allYachts
            };
        }

        public async Task<IEnumerable<VehicleAllViewModel>> AllByAgentIdAsync(string agentId)
        {
            IEnumerable<VehicleAllViewModel> allAgentYachts = await this.dbContext
                .Yachts
                .Where(y => y.IsActive && y.AgentId.ToString() == agentId)
                .Select(y => new VehicleAllViewModel
                {
                    Id = y.Id.ToString(),
                    Brand = y.Brand,
                    Model = y.Model,
                    RegistrationNumber = y.RegistrationNumber,
                    Address = y.Address,
                    Color = y.Color,
                    ImageUrl = y.ImageUrl,
                    PricePerDay = y.PricePerDay,
                    IsRented = y.RenterId.HasValue
                })
                .ToArrayAsync();

            return allAgentYachts;
        }

        public async Task<IEnumerable<VehicleAllViewModel>> AllByUserIdAsync(string userId)
        {
            IEnumerable<VehicleAllViewModel> allUserYachts = await this.dbContext
                .Yachts
                .Where(y => y.IsActive &&
                                y.RenterId.HasValue &&
                                y.RenterId.ToString() == userId)
                .Select(y => new VehicleAllViewModel
                {
                    Id = y.Id.ToString(),
                    Brand = y.Brand,
                    Model = y.Model,
                    RegistrationNumber = y.RegistrationNumber,
                    Address = y.Address,
                    Color = y.Color,
                    ImageUrl = y.ImageUrl,
                    PricePerDay = y.PricePerDay,
                    IsRented = y.RenterId.HasValue
                })
                .ToArrayAsync();

            return allUserYachts;
        }

        public async Task<VehicleDetailsViewModel> GetDetailsByIdAsync(string yachtId)
        {
            Yacht yacht = await this.dbContext
                .Yachts
                .Include(y => y.Category)
                .Include(y => y.Agent)
                .ThenInclude(a => a.User)
                .Where(y => y.IsActive)
                .FirstAsync(y => y.Id.ToString() == yachtId);
            
            return new VehicleDetailsViewModel
            {
                Id = yacht.Id.ToString(),
                Brand = yacht.Brand,
                Model = yacht.Model,
                RegistrationNumber = yacht.RegistrationNumber,
                Address = yacht.Address,
                Color = yacht.Color,
                ImageUrl = yacht.ImageUrl,
                PricePerDay = yacht.PricePerDay,
                IsRented = yacht.RenterId.HasValue,
                Category = yacht.Category.Name,
                Agent = new AgentInfoOnVehicleViewModel
                {
                    Email = yacht.Agent.User.Email,
                    PhoneNumber = yacht.Agent.PhoneNumber,
                    Address = yacht.Agent.Address,
                }
            };
        }

        public async Task<bool> ExistByIdAsync(string yachtId)
        {
            return await this.dbContext
                .Yachts
                .Where(y => y.IsActive)
                .AnyAsync(y => y.Id.ToString() == yachtId);
        }

        public async Task<VehicleFormModel> GetYachtForEditByIdAsync(string yachtId)
        {
            Yacht yacht = await this.dbContext
                .Yachts
                .Include(y => y.Category)
                .Where(y => y.IsActive)
                .FirstAsync(y => y.Id.ToString() == yachtId);

            return new VehicleFormModel
            {
                Brand = yacht.Brand,
                Model = yacht.Model,
                RegistrationNumber = yacht.RegistrationNumber,
                Address = yacht.Address,
                PricePerDay = yacht.PricePerDay,
                ImageUrl = yacht.ImageUrl,
                Color = yacht.Color,
                CategoryId = yacht.CategoryId
            };
        }

        public async Task<bool> IsAgentWithIdOwnerOfYachtWithIdAsync(string yachtId, string agentId)
        {
            Yacht yacht = await this.dbContext
                .Yachts
                .Where(y => y.IsActive)
                .FirstAsync(y => y.Id.ToString() == yachtId);

            return yacht.AgentId.ToString() == agentId;
        }

        public async Task EditYachtByIdAndFormModelAsync(string yachtId, VehicleFormModel yachtFormModel)
        {
            Yacht yacht = await this.dbContext
                .Yachts
                .Where(y => y.IsActive)
                .FirstAsync(y => y.Id.ToString() == yachtId);

            yacht.Brand = yachtFormModel.Brand;
            yacht.Model = yachtFormModel.Model;
            yacht.RegistrationNumber = yachtFormModel.RegistrationNumber;
            yacht.Address = yachtFormModel.Address;
            yacht.PricePerDay = yachtFormModel.PricePerDay;
            yacht.ImageUrl = yachtFormModel.ImageUrl;
            yacht.Color = yachtFormModel.Color;
            yacht.CategoryId = yachtFormModel.CategoryId;

            await this.dbContext.SaveChangesAsync();
        }
    }
}
