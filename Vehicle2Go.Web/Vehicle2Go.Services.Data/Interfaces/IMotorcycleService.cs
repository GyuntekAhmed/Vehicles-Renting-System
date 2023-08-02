namespace Vehicle2Go.Services.Data.Interfaces
{
    using Models.Vehicle;
    using Models.Statistics;
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
        Task<VehiclePreDeleteDetailsViewModel> GetMotorcycleForDeleteByIdAsync(string motorcycleId);
        Task DeleteByIdAsync(string motorcycleId);
        Task<bool> IsRentedByIdAsync(string motorcycleId);
        Task RentMotorcycleAsync(string motorcycleId, string userId);
        Task<bool> IsRentedByUserWithIdAsync(string motorcycleId, string userId);
        Task LeaveAsync(string motorcycleId);
        Task<StatisticsServiceModel> GetStatisticsAsync();
        Task<bool> HasMotorcycleWithIdAsync(string userId, string motorcycleId);
    }
}
