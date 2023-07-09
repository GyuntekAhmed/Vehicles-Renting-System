namespace Vehicle2Go.Services.Data.Interfaces
{
    using Web.ViewModels.Category;

    public interface ICarCategoryService
    {
        Task<IEnumerable<VehicleSelectCategoryViewModel>> AllCategoriesAsync();
    }
}
