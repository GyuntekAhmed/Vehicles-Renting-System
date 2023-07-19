namespace Vehicle2Go.Services.Data.Interfaces
{
    using Vehicle2Go.Web.ViewModels.Home;

    public interface IVehicleService
    {
        Task<IEnumerable<IndexViewModel>> AllVehiclesAsync();
    }
}
