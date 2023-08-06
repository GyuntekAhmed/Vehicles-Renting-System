namespace Vehicle2Go.Services.Data.Interfaces
{
    using Web.ViewModels.User;

    public interface IUserService
    {
        Task<string> GetFullNameAsync(string email);
        Task<string> GetFullNameByIdAsync(string userId);
        Task<IEnumerable<UserViewModel>> AllUsersAsync();
    }
}
