namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using Vehicle2Go.Data.Models;
    using Web.ViewModels.Category;
    using Web.ViewModels.Vehicle;
    using Interfaces;
    using Web.ViewModels.Home;
    using Vehicle2Go.Data;

    public class MotorcycleService : IMotorcycleService
    {
        private readonly VehicleDbContext dbContext;

        public MotorcycleService(VehicleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexViewModel>> AllMotorcyclesAsync()
        {
            return await dbContext
                .Motorcycles
                .Select(m => new IndexViewModel()
                {
                    Id = m.Id.ToString(),
                    Brand = m.Brand,
                    ImageUrl = m.ImageUrl,
                })
                .ToArrayAsync();
        }

        public async Task<IEnumerable<VehicleSelectCategoryViewModel>> AllCategoriesAsync()
        {
            IEnumerable<VehicleSelectCategoryViewModel> allCategories = await dbContext
                .MotorcycleCategories
                .AsNoTracking()
                .Select(m => new VehicleSelectCategoryViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                })
                .ToArrayAsync();

            return allCategories;
        }

        public async Task<bool> CategoryExistByIdAsync(int id)
        {
            return await dbContext
                .MotorcycleCategories
                .AnyAsync(m => m.Id == id);
        }

        public async Task CreateAsync(AddVehicleViewModel viewModel, string agentId)
        {
            Motorcycle newMotorcycle = new Motorcycle()
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

            await dbContext.Motorcycles.AddAsync(newMotorcycle);
            await dbContext.SaveChangesAsync();
        }
    }
}
