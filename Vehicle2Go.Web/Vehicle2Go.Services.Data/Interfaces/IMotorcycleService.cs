namespace Vehicle2Go.Services.Data.Interfaces
{
    using Models.Vehicle;
    using Web.ViewModels.Vehicle;

    public interface IMotorcycleService
    {
        Task<string> CreateAndReturnIdAsync(VehicleFormModel formModel, string agentId);
        Task<AllVehiclesFilteredAndPagedServiceModel> AllAsync(AllVehiclesQueryModel queryModel);
        Task<IEnumerable<VehicleAllViewModel>> AllByAgentIdAsync(string agentId);
        Task<IEnumerable<VehicleAllViewModel>> AllByUserIdAsync(string userId);
        Task<VehicleDetailsViewModel> GetDetailsByIdAsync(string motorcycleId);
        Task<bool> ExistByIdAsync(string motorcycleId);
        Task<VehicleFormModel> GetMotorcycleForEditByIdAsync(string motorcycleId);
        Task<bool> IsAgentWithIdOwnerOfMotorcycleWithIdAsync(string motorcycleId, string agentId);
        Task EditMotorcycleByIdAndFormModelAsync(string motorcycleId, VehicleFormModel motorcycleFormModel);
    }
}
