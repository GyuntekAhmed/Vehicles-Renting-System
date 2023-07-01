using Microsoft.EntityFrameworkCore;

namespace VehiclesRenting.Services
{
    using System.Threading.Tasks;

    using Data;
    using Interfaces;
    using Web.ViewModels.Category;

    public class CategoryService : ICategoryService
    {
        private readonly VehiclesRentingDbContext dbContext;

        public CategoryService(VehiclesRentingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<VehicleCategoryViewModel>> AllCategoriesAsync()
        {
            return await dbContext.Categories
                .AsNoTracking()
                .Select(c => new VehicleCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToArrayAsync();
        }
    }
}
