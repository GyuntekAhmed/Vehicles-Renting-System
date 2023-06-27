namespace VehiclesRenting.Services.Interfaces
{
    using Web.ViewModels.Agent;

    public interface IAgentService
    {
        Task<bool> AgentExistByIdAsync(string agentId);
        Task<bool> AgentExistsByPhoneAsync(string phoneNumber);
        Task<bool> UserHasRentsAsync(string userId);
        Task Create(string userId, BecomeAgentFormModel model);
    }
}
