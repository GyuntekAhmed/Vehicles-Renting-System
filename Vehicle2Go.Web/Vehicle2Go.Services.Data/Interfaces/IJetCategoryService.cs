﻿namespace Vehicle2Go.Services.Data.Interfaces
{
    using Web.ViewModels.Category;

    public interface IJetCategoryService
    {
        Task<IEnumerable<SelectCategoryFormModel>> AllCategoriesAsync();
        Task<bool> ExistByIdAsync(int id);
    }
}
