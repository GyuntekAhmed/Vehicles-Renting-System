namespace Vehicle2Go.Services.Data.Interfaces
{
    using Web.ViewModels.Car;
    using Web.ViewModels.Home;

    public interface ICarService
    {
        Task CreateAsync(CarFormModel formModel, string agentId);
    }
}
