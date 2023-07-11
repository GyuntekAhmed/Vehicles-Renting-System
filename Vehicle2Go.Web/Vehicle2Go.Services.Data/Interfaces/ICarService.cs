namespace Vehicle2Go.Services.Data.Interfaces
{
    using Web.ViewModels.Vehicle;
    using Web.ViewModels.Category;
    using Web.ViewModels.Home;

    public interface ICarService
    {
        Task<IEnumerable<IndexViewModel>> AllCarsAsync();
        Task<IEnumerable<VehicleSelectCategoryViewModel>> AllCategoriesAsync();
        Task<bool> CategoryExistByIdAsync(int id);
        Task CreateAsync(AddVehicleViewModel viewModel, string agentId);
    }
}
