using Vehicle2Go.Web.ViewModels.Category;

namespace Vehicle2Go.Services.Data.Interfaces
{
    public interface ICarCategoryService
    {
        Task<IEnumerable<SelectCategoryFormModel>> AllCategoriesAsync();
        Task<bool> ExistByIdAsync(int id);
    }
}
