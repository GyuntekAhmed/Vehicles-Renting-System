namespace Vehicle2Go.Services.Data.Interfaces
{
    using Web.ViewModels.Category;
    using Web.ViewModels.Home;

    public interface IJetService
    {
        Task<IEnumerable<IndexViewModel>> AllJetsAsync();
        Task<IEnumerable<VehicleSelectCategoryViewModel>> AllCategoriesAsync();
    }
}
