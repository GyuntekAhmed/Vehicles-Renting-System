namespace Vehicle2Go.Services.Data.Interfaces
{
    using Models.Vehicle;
    using Models.Statistics;
    using Web.ViewModels.Vehicle;

    public interface ITruckService
    {
        Task<string> CreateAndReturnIdAsync(VehicleFormModel formModel, string agentId);
        Task<AllVehiclesFilteredAndPagedServiceModel> AllAsync(AllVehiclesQueryModel queryModel);
        Task<IEnumerable<VehicleAllViewModel>> AllByAgentIdAsync(string agentId);
        Task<IEnumerable<VehicleAllViewModel>> AllByUserIdAsync(string userId);
        Task<VehicleDetailsViewModel> GetDetailsByIdAsync(string truckId);
        Task<bool> ExistByIdAsync(string truckId);
        Task<VehicleFormModel> GetTruckForEditByIdAsync(string truckId);
        Task<bool> IsAgentWithIdOwnerOfTruckWithIdAsync(string truckId, string agentId);
        Task EditTruckByIdAndFormModelAsync(string truckId, VehicleFormModel truckFormModel);
        Task<VehiclePreDeleteDetailsViewModel> GetTruckForDeleteByIdAsync(string truckId);
        Task DeleteByIdAsync(string truckId);
        Task<bool> IsRentedByIdAsync(string truckId);
        Task RentTruckAsync(string truckId, string userId);
        Task<bool> IsRentedByUserWithIdAsync(string truckId, string userId);
        Task LeaveAsync(string truckId);
        Task<StatisticsServiceModel> GetStatisticsAsync();
        Task<bool> HasTruckWithIdAsync(string userId, string truckId);
    }
}
