namespace VehiclesRenting.Services.Interfaces
{
    using Web.ViewModels.Home;

    public interface IVehicleService
    {
        Task<IEnumerable<IndexViewModel>> AllVehiclesAsync();
    }
}
