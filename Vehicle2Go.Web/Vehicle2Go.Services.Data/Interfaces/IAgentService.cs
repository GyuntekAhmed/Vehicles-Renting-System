namespace Vehicle2Go.Services.Data.Interfaces
{
    using Web.ViewModels.Agent;

    public interface IAgentService
    {
        Task<bool> AgentExistByUserIdAsync(string userId);
        Task<bool> AgentExistByPhoneNumberAsync(string phoneNumber);
        Task Create(string userId, BecomeAgentFormModel model);
        Task<string?> GetAgentIdByUserIdAsync(string userId);
        Task<bool> HasVehicleWithIdAsync(string? userId, string vehicleId);
    }
}
