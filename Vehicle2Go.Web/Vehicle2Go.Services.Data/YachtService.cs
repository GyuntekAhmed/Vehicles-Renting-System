namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using Vehicle2Go.Data.Models;
    using Web.ViewModels.Category;
    using Web.ViewModels.Vehicle;
    using Interfaces;
    using Web.ViewModels.Home;
    using Vehicle2Go.Data;

    public class YachtService : IYachtService
    {
        private readonly VehicleDbContext dbContext;

        public YachtService(VehicleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexViewModel>> AllCarsAsync()
        {
            return await dbContext
                .Yachts
                .Select(y => new IndexViewModel()
                {
                    Id = y.Id.ToString(),
                    Brand = y.Brand,
                    ImageUrl = y.ImageUrl,
                })
                .ToArrayAsync();
        }

        public async Task<IEnumerable<VehicleSelectCategoryViewModel>> AllCategoriesAsync()
        {
            IEnumerable<VehicleSelectCategoryViewModel> allCategories = await dbContext
                .YachtCategories
                .AsNoTracking()
                .Select(y => new VehicleSelectCategoryViewModel()
                {
                    Id = y.Id,
                    Name = y.Name,
                })
                .ToArrayAsync();

            return allCategories;
        }

        public async Task<bool> CategoryExistByIdAsync(int id)
        {
            return await dbContext
                .YachtCategories
                .AnyAsync(y => y.Id == id);
        }

        public async Task CreateAsync(AddVehicleViewModel viewModel, string agentId)
        {
            Yacht newYacht = new Yacht()
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

            await dbContext.Yachts.AddAsync(newYacht);
            await dbContext.SaveChangesAsync();
        }
    }
}
