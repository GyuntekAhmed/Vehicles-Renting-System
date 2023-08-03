namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using Interfaces;
    using Vehicle2Go.Data;
    using Vehicle2Go.Data.Models.User;

    public class UserService : IUserService
    {
        private readonly Vehicle2GoDbContext dDbContext;

        public UserService(Vehicle2GoDbContext dDbContext)
        {
            this.dDbContext = dDbContext;
        }

        public async Task<string> GetFullNameAsync(string email)
        {
            ApplicationUser? user = await this.dDbContext
                .Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
        }
    }
}
