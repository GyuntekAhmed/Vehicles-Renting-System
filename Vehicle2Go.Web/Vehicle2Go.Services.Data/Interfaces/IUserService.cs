namespace Vehicle2Go.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<string> GetFullNameAsync(string email);
    }
}
