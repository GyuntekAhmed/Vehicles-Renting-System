namespace Vehicle2Go.Services.Data.Interfaces
{
    using Models.Vehicle;
    using Models.Statistics;
    using Web.ViewModels.Vehicle;

    public interface IYachtService
    {
        Task<string> CreateAndReturnIdAsync(VehicleFormModel formModel, string agentId);
        Task<AllVehiclesFilteredAndPagedServiceModel> AllAsync(AllVehiclesQueryModel queryModel);
        Task<IEnumerable<VehicleAllViewModel>> AllByAgentIdAsync(string agentId);
        Task<IEnumerable<VehicleAllViewModel>> AllByUserIdAsync(string userId);
        Task<VehicleDetailsViewModel> GetDetailsByIdAsync(string yachtId);
        Task<bool> ExistByIdAsync(string yachtId);
        Task<VehicleFormModel> GetYachtForEditByIdAsync(string yachtId);
        Task<bool> IsAgentWithIdOwnerOfYachtWithIdAsync(string yachtId, string agentId);
        Task EditYachtByIdAndFormModelAsync(string yachtId, VehicleFormModel yachtFormModel);
        Task<VehiclePreDeleteDetailsViewModel> GetYachtForDeleteByIdAsync(string yachtId);
        Task DeleteByIdAsync(string yachtId);
        Task<bool> IsRentedByIdAsync(string yachtId);
        Task RentYachtAsync(string yachtId, string userId);
        Task<bool> IsRentedByUserWithIdAsync(string yachtId, string userId);
        Task LeaveAsync(string yachtId);
        Task<StatisticsServiceModel> GetStatisticsAsync();
        Task<bool> HasYachtWithIdAsync(string userId, string yachtId);
    }
}
