namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using Models.Vehicle;
    using Web.ViewModels.Vehicle;
    using Interfaces;
    using Vehicle2Go.Data;
    using Vehicle2Go.Data.Models.Vehicle;
    using Web.ViewModels.Vehicle.Enums;

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
                    .OrderBy(y => y.CreatedOn),
                VehicleSorting.Oldest => yachtQuery
                .OrderByDescending(y => y.CreatedOn),
                VehicleSorting.PriceAscending => yachtQuery
                    .OrderBy(y => y.PricePerDay),
                VehicleSorting.PriceDescending => yachtQuery
                    .OrderByDescending(y => y.PricePerDay),
                _ => yachtQuery
                    .OrderBy(y => y.RenterId != null)
                    .ThenByDescending(y => y.CreatedOn),
            };

            IEnumerable<VehicleAllViewModel> allYachts = await yachtQuery
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
    }
}
