namespace Vehicle2Go.Services.Data.Interfaces
{
    using Web.ViewModels.Home;

    public interface IVehicleService
    {
        Task<IEnumerable<IndexViewModel>> AllVehiclesAsync();
    }
}
