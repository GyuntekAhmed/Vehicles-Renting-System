namespace VehiclesRenting.Services.Interfaces
{
    using VehiclesRenting.Web.ViewModels.Home;

    public interface IMotorcycleService
    {
        Task<IEnumerable<IndexViewModel>> AllMotorcyclesAsync();
    }
}
