namespace VehiclesRenting.Services.Interfaces
{
    using Web.ViewModels.Home;

    public interface IMotorcycleService
    {
        IEnumerable<MotorcycleIndexViewModel> AllMotorcyclesAsync();
    }
}
