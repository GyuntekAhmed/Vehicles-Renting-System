namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using Web.ViewModels.Vehicle.Enums;
    using Web.ViewModels.Vehicle;
    using Interfaces;
    using Vehicle2Go.Data;
    using Vehicle2Go.Data.Models.Vehicle;
    using Models.Vehicle;

    public class CarService : ICarService
    {
        private readonly Vehicle2GoDbContext dbContext;

        public CarService(Vehicle2GoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(VehicleFormModel formModel, string agentId)
        {
            Car newCar = new Car
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

            await this.dbContext.Cars.AddAsync(newCar);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<AllVehiclesFilteredAndPagedServiceModel> AllAsync(AllVehiclesQueryModel queryModel)
        {
            IQueryable<Car> carQuery = this.dbContext
                .Cars
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                carQuery = carQuery
                    .Where(c => c.Category.Name == queryModel.Category);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";

                carQuery = carQuery
                    .Where
                    (c => EF.Functions.Like(c.Brand, wildCard) ||
                          EF.Functions.Like(c.Model, wildCard) ||
                          EF.Functions.Like(c.Address, wildCard) ||
                          EF.Functions.Like(c.RegistrationNumber, wildCard)||
                          EF.Functions.Like(c.Color, wildCard));
            }

            carQuery = queryModel.VehicleSorting switch
            {
                VehicleSorting.Newest => carQuery
                    .OrderByDescending(c => c.CreatedOn),
                VehicleSorting.Oldest => carQuery
                .OrderBy(c => c.CreatedOn),
                VehicleSorting.PriceAscending => carQuery
                    .OrderBy(c => c.PricePerDay),
                VehicleSorting.PriceDescending => carQuery
                    .OrderByDescending(c => c.PricePerDay),
                _ => carQuery
                    .OrderBy(c => c.RenterId != null)
                    .ThenByDescending(c => c.CreatedOn),
            };

            IEnumerable<VehicleAllViewModel> allCars = await carQuery
                .Where(c => c.IsActive)
                .Skip((queryModel.CurrentPage - 1) * queryModel.VehiclesPerPage)
                .Take(queryModel.VehiclesPerPage)
                .Select(c => new VehicleAllViewModel
                {
                    Id = c.Id.ToString(),
                    Brand = c.Brand,
                    Model = c.Model,
                    RegistrationNumber = c.RegistrationNumber,
                    Address = c.Address,
                    Color = c.Color,
                    ImageUrl = c.ImageUrl,
                    PricePerDay = c.PricePerDay,
                    IsRented = c.RenterId.HasValue
                })
                .ToArrayAsync();

            int totalCars = carQuery.Count();

            return new AllVehiclesFilteredAndPagedServiceModel
            {
                TotalVehiclesCount = totalCars,
                Vehicles = allCars
            };
        }

        public async Task<IEnumerable<VehicleAllViewModel>> AllByAgentIdAsync(string agentId)
        {
            IEnumerable<VehicleAllViewModel> allAgentCars = await this.dbContext
                .Cars
                .Where(c => c.AgentId.ToString() == agentId)
                .Select(c => new VehicleAllViewModel
                {
                    Id = c.Id.ToString(),
                    Brand = c.Brand,
                    Model = c.Model,
                    RegistrationNumber = c.RegistrationNumber,
                    Address = c.Address,
                    Color = c.Color,
                    ImageUrl = c.ImageUrl,
                    PricePerDay = c.PricePerDay,
                    IsRented = c.RenterId.HasValue
                })
                .ToArrayAsync();

            return allAgentCars;
        }

        public async Task<IEnumerable<VehicleAllViewModel>> AllByUserIdAsync(string userId)
        {
            IEnumerable<VehicleAllViewModel> allUserCars = await this.dbContext
                .Cars
                .Where(c => c.RenterId.HasValue &&
                                c.RenterId.ToString() == userId)
                .Select(c => new VehicleAllViewModel
                {
                    Id = c.Id.ToString(),
                    Brand = c.Brand,
                    Model = c.Model,
                    RegistrationNumber = c.RegistrationNumber,
                    Address = c.Address,
                    Color = c.Color,
                    ImageUrl = c.ImageUrl,
                    PricePerDay = c.PricePerDay,
                    IsRented = c.RenterId.HasValue
                })
                .ToArrayAsync();

            return allUserCars;
        }
    }
}
