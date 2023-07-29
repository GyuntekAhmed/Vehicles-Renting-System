﻿namespace Vehicle2Go.Services.Data.Interfaces
{
    using Models.Vehicle;
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
    }
}
