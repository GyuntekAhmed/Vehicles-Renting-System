namespace Vehicle2Go.Services.Data.Interfaces
{
    using Web.ViewModels.Vehicle;

    public interface IYachtService
    {
        Task CreateAsync(VehicleFormModel formModel, string agentId);
    }
}
