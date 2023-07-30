namespace Vehicle2Go.Services.Data.Interfaces
{
    using Models.Vehicle;
    using Web.ViewModels.Vehicle;

    public interface ICarService
    {
        Task<string> CreateAndReturnIdAsync(VehicleFormModel formModel, string agentId);
        Task<AllVehiclesFilteredAndPagedServiceModel> AllAsync(AllVehiclesQueryModel queryModel);
        Task<IEnumerable<VehicleAllViewModel>> AllByAgentIdAsync(string agentId);
        Task<IEnumerable<VehicleAllViewModel>> AllByUserIdAsync(string userId);
        Task<VehicleDetailsViewModel> GetDetailsByIdAsync(string carId);
        Task<bool> ExistByIdAsync(string carId);
        Task<VehicleFormModel> GetCarForEditByIdAsync(string carId);
        Task<bool> IsAgentWithIdOwnerOfCarWithIdAsync(string carId, string agentId);
        Task EditCarByIdAndFormModelAsync( string carId, VehicleFormModel carFormModel);
        Task<VehiclePreDeleteDetailsViewModel> GetCarForDeleteByIdAsync(string carId);
        Task DeleteByIdAsync(string carId);
        Task<bool> IsRentedByIdAsync(string carId);
        Task RentCarAsync(string carId, string userId);
        Task<bool> IsRentedByUserWithIdAsync(string carId, string userId);
        Task LeaveAsync(string carId);
    }
}
