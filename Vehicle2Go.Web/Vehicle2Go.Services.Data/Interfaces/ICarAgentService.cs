namespace Vehicle2Go.Services.Data.Interfaces
{
    public interface ICarAgentService
    {
        Task<bool> AgentExistByUserIdAsync(string userId);
    }
}
