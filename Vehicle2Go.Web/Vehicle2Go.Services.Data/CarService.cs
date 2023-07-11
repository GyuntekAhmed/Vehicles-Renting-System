namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using Vehicle2Go.Data.Models;
    using Web.ViewModels.Category;
    using Web.ViewModels.Vehicle;
    using Interfaces;
    using Web.ViewModels.Home;
    using Vehicle2Go.Data;

    public class CarService : ICarService
    {
        private readonly VehicleDbContext dbContext;

        public CarService(VehicleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public async Task<IEnumerable<VehicleSelectCategoryViewModel>> AllCategoriesAsync()
        {
            IEnumerable<VehicleSelectCategoryViewModel> allCategories = await dbContext
                .CarCategories
                .AsNoTracking()
                .Select(c => new VehicleSelectCategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToArrayAsync();

            return allCategories;
        }

        public async Task<bool> CategoryExistByIdAsync(int id)
        {
            return await dbContext
                .CarCategories
                .AnyAsync(c => c.Id == id);
        }

        public async Task CreateAsync(AddVehicleViewModel viewModel, string agentId)
        {
            Car newCar = new Car
            {
                Brand = viewModel.Brand,
                Model = viewModel.Model,
                RegistrationNumber = viewModel.RegistrationNumber,
                Address = viewModel.Address,
                PricePerDay = viewModel.PricePerDay,
                Color = viewModel.Color,
                ImageUrl = viewModel.ImageUrl,
                CategoryId = viewModel.CategoryId,
                AgentId = Guid.Parse(agentId),
            };

            await dbContext.Cars.AddAsync(newCar);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<IndexViewModel>> AllCarsAsync()
        {
            return await dbContext
                .Cars
                .Select(c => new IndexViewModel
                {
                    Id = c.Id.ToString(),
                    Brand = c.Brand,
                    ImageUrl = c.ImageUrl,
                })
                .ToArrayAsync();
        }
    }
}
