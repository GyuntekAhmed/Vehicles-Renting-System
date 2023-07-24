namespace Vehicle2Go.Services.Data
{
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Vehicle2Go.Data; 
    using Web.ViewModels.Category;

    public class JetCategoryService : IJetCategoryService
    {
        private readonly Vehicle2GoDbContext dbContext;

        public JetCategoryService(Vehicle2GoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<SelectCategoryFormModel>> AllCategoriesAsync()
        {
            IEnumerable<SelectCategoryFormModel> allCategories = await this.dbContext
                .JetCategories
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
                .JetCategories
                .AnyAsync(c => c.Id == id);

            return result;
        }

        public async Task<IEnumerable<string>> AllCategoryNamesAsync()
        {
            IEnumerable<string> allNames = await this.dbContext
                .JetCategories
                .Select(c => c.Name)
                .ToArrayAsync();

            return allNames;
        }
    }
}
