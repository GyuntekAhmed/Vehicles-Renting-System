namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using Vehicle2Go.Data;
    using Interfaces;
    using Web.ViewModels.Category;

    public class CarCategoryService : ICarCategoryService
    {
        private readonly VehicleDbContext dbContext;

        public CarCategoryService(VehicleDbContext dbContext)
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
    }
}
