namespace Vehicle2Go.Services.Data
{
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Vehicle2Go.Data;
    using Web.ViewModels.Category;

    public class YachtCategoryService : IYachtCategoryService
    {
        private readonly Vehicle2GoDbContext dbContext;

        public YachtCategoryService(Vehicle2GoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<SelectCategoryFormModel>> AllCategoriesAsync()
        {
            IEnumerable<SelectCategoryFormModel> allCategories = await this.dbContext
                .YachtCategories
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
                .YachtCategories
                .AnyAsync(c => c.Id == id);

            return result;
        }
    }
}
