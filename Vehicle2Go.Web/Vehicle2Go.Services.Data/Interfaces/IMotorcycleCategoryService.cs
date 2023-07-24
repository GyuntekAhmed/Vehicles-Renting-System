namespace Vehicle2Go.Services.Data.Interfaces
{
    using Web.ViewModels.Category;

    public interface IMotorcycleCategoryService
    {
        Task<IEnumerable<SelectCategoryFormModel>> AllCategoriesAsync();
        Task<bool> ExistByIdAsync(int id);
        Task<IEnumerable<string>> AllCategoryNamesAsync();
    }
}
