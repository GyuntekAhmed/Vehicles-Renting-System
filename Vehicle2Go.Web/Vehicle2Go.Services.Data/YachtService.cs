using Vehicle2Go.Web.ViewModels.Category;

namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;

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
    }
}
