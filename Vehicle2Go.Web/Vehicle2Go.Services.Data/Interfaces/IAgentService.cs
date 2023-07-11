namespace Vehicle2Go.Services.Data.Interfaces
{
    using Web.ViewModels.Agent;

    public interface IAgentService
    {
        Task<bool> AgentExistByUserIdAsync(string userId);
        Task<bool> AgentExistByPhoneNumberAsync(string phoneNumber);
        Task<bool> HasRentsByUserIdAsync(string userId);
        Task Create(string userId, BecomeAgentFormModel model);
        Task<string?> GetAgentIdAsync(string userId);
    }
}
