namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using Vehicle2Go.Data.Models;
    using Web.ViewModels.Category;
    using Web.ViewModels.Vehicle;
    using Interfaces;
    using Web.ViewModels.Home;
    using Vehicle2Go.Data;

    public class JetService : IJetService
    {
        private readonly VehicleDbContext dbContext;

        public JetService(VehicleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<VehicleSelectCategoryViewModel>> AllCategoriesAsync()
        {
            IEnumerable<VehicleSelectCategoryViewModel> allCategories = await dbContext
                .JetCategories
                .AsNoTracking()
                .Select(j => new VehicleSelectCategoryViewModel()
                {
                    Id = j.Id,
                    Name = j.Name,
                })
                .ToArrayAsync();

            return allCategories;
        }

        public async Task<bool> CategoryExistByIdAsync(int id)
        {
            return await dbContext
                .JetCategories
                .AnyAsync(j => j.Id == id);
        }

        public async Task CreateAsync(AddVehicleViewModel viewModel, string agentId)
        {
            Jet newJet = new Jet()
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

            await dbContext.Jets.AddAsync(newJet);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<IndexViewModel>> AllJetsAsync()
        {
            return await dbContext
                .Jets
                .Select(j => new IndexViewModel
                {
                    Id = j.Id.ToString(),
                    Brand = j.Brand,
                    ImageUrl = j.ImageUrl,
                })
                .ToArrayAsync();
        }
    }
}
