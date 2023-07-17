namespace Vehicle2Go.Services.Data.Interfaces
{
    using Web.ViewModels.Car;
    using Web.ViewModels.Home;

    public interface ICarService
    {
        Task<IEnumerable<IndexViewModel>> AllVehiclesAsync();
        Task CreateAsync(CarFormModel formModel, string agentId);
    }
}
