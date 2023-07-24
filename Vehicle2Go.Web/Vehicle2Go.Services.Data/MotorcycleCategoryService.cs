namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using Vehicle2Go.Data;
    using Interfaces;
    using Web.ViewModels.Category;

    public class MotorcycleCategoryService : IMotorcycleCategoryService
    {
        private readonly Vehicle2GoDbContext dbContext;

        public MotorcycleCategoryService(Vehicle2GoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<SelectCategoryFormModel>> AllCategoriesAsync()
        {
            IEnumerable<SelectCategoryFormModel> allCategories = await this.dbContext
                .MotorcycleCategories
                .AsNoTracking()
                .Select(c => new SelectCategoryFormModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToArrayAsync();

            return allCategories;
        }

        public async Task<bool> ExistByIdAsync(int id)
        {
            bool result = await this.dbContext
                .MotorcycleCategories
                .AnyAsync(c => c.Id == id);

            return result;
        }

        public async Task<IEnumerable<string>> AllCategoryNamesAsync()
        {
            IEnumerable<string> allNames = await this.dbContext
                .MotorcycleCategories
                .Select(c => c.Name)
                .ToArrayAsync();

            return allNames;
        }
    }
}
