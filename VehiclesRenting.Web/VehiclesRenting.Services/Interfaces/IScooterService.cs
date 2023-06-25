namespace VehiclesRenting.Services.Interfaces
{
    using Web.ViewModels.Home;

    public interface IScooterService
    {
        Task<IEnumerable<ScooterIndexViewModel>> AllScootersAsync();
    }
}
