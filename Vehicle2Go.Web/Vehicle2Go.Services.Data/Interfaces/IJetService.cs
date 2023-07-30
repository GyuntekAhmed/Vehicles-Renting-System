namespace Vehicle2Go.Services.Data.Interfaces
{
    using Models.Vehicle;
    using Models.Statistics;
    using Web.ViewModels.Vehicle;

    public interface IJetService
    {
        Task<string> CreateAndReturnIdAsync(VehicleFormModel formModel, string agentId);
        Task<AllVehiclesFilteredAndPagedServiceModel> AllAsync(AllVehiclesQueryModel queryModel);
        Task<IEnumerable<VehicleAllViewModel>> AllByAgentIdAsync(string agentId);
        Task<IEnumerable<VehicleAllViewModel>> AllByUserIdAsync(string userId);
        Task<VehicleDetailsViewModel> GetDetailsByIdAsync(string jetId);
        Task<bool> ExistByIdAsync(string jetId);
        Task<VehicleFormModel> GetJetForEditByIdAsync(string jetId);
        Task<bool> IsAgentWithIdOwnerOfJetWithIdAsync(string jetId, string agentId);
        Task EditJetByIdAndFormModelAsync(string jetId, VehicleFormModel jetFormModel);
        Task<VehiclePreDeleteDetailsViewModel> GetJetForDeleteByIdAsync(string jetId);
        Task DeleteByIdAsync(string jetId);
        Task<bool> IsRentedByIdAsync(string jetId);
        Task RentJetAsync(string jetId, string userId);
        Task<bool> IsRentedByUserWithIdAsync(string jetId, string userId);
        Task LeaveAsync(string jetId);
        Task<StatisticsServiceModel> GetStatisticsAsync();
    }
}
