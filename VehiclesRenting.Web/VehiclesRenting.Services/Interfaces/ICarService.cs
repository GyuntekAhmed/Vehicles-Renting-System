namespace VehiclesRenting.Services.Interfaces
{
    using Web.ViewModels.Home;

    public interface ICarService
    {
        Task<IEnumerable<CarIndexViewModel>> AllCarsAsync();
    }
}
