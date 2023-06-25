namespace VehiclesRenting.Services.Interfaces
{
    using Web.ViewModels.Home;

    public interface ICarService
    {
        Task<IEnumerable<IndexViewModel>> AllCarsAsync();
    }
}
