namespace Vehicle2Go.Services.Data.Interfaces
{
    using Models.Vehicle;
    using Web.ViewModels.Vehicle;

    public interface IYachtService
    {
        Task CreateAsync(VehicleFormModel formModel, string agentId);
        Task<AllVehiclesFilteredAndPagedServiceModel> AllAsync(AllVehiclesQueryModel queryModel);
    }
}
