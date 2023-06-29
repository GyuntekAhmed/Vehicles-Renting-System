namespace VehiclesRenting.Services.Interfaces
{
    using Web.ViewModels.Car;

    public interface ICarService
    {
        Task<IEnumerable<AllCarsViewModel>> AllCarsAsync();
    }
}
