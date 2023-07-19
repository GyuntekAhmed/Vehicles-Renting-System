namespace Vehicle2Go.Services.Data.Interfaces
{
    using Web.ViewModels.Vehicle;

    public interface IMotorcycleService
    {
        Task CreateAsync(VehicleFormModel formModel, string agentId);
    }
}
