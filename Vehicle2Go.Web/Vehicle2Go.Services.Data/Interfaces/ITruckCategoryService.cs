namespace Vehicle2Go.Services.Data.Interfaces
{
    using Web.ViewModels.Category;

    public interface ITruckCategoryService
    {
        Task<IEnumerable<SelectCategoryFormModel>> AllCategoriesAsync();
        Task<bool> ExistByIdAsync(int id);
    }
}
