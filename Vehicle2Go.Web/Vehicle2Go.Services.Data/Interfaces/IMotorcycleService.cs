namespace Vehicle2Go.Services.Data.Interfaces
{
    using Web.ViewModels.Category;
    using Web.ViewModels.Home;

    public interface IMotorcycleService
    {
        Task<IEnumerable<IndexViewModel>> AllMotorcyclesAsync();
        Task<IEnumerable<VehicleSelectCategoryViewModel>> AllCategoriesAsync();
    }
}
