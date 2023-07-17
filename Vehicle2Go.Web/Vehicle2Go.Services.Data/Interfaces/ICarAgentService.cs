using Vehicle2Go.Web.ViewModels.Agent;

namespace Vehicle2Go.Services.Data.Interfaces
{
    public interface ICarAgentService
    {
        Task<bool> AgentExistByUserIdAsync(string userId);
        Task<bool> AgentExistByPhoneNumberAsync(string phoneNumber);
        Task Create(string userId, BecomeAgentFormModel model);
        Task<string?> GetAgentIdByUserIdAsync(string userId);
    }
}
