namespace Vehicle2Go.Services.Data.Interfaces
{
    using Web.ViewModels.Vehicle;

    public interface ITruckService
    {
        Task CreateAsync(VehicleFormModel formModel, string agentId);
    }
}
