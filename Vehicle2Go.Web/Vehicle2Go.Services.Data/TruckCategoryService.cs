﻿namespace Vehicle2Go.Services.Data
{
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Vehicle2Go.Data;
    using Web.ViewModels.Category;

    public class TruckCategoryService : ITruckCategoryService
    {
        private readonly Vehicle2GoDbContext dbContext;

        public TruckCategoryService(Vehicle2GoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<SelectCategoryFormModel>> AllCategoriesAsync()
        {
            IEnumerable<SelectCategoryFormModel> allCategories = await this.dbContext
                .TruckCategories
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
                .TruckCategories
                .AnyAsync(c => c.Id == id);

            return result;
        }

        public async Task<IEnumerable<string>> AllCategoryNamesAsync()
        {
            IEnumerable<string> allNames = await this.dbContext
                .TruckCategories
                .Select(c => c.Name)
                .ToArrayAsync();

            return allNames;
        }
    }
}
