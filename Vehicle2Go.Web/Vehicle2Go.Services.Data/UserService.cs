namespace Vehicle2Go.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using Interfaces;
    using Vehicle2Go.Data;
    using Vehicle2Go.Data.Models.User;
    using Vehicle2Go.Data.Models.Agent;
    using Web.ViewModels.User;

    using static Vehicle2Go.Common.EntityValidationConstants;

    public class UserService : IUserService
    {
        private readonly Vehicle2GoDbContext dbContext;

        public UserService(Vehicle2GoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> GetFullNameAsync(string email)
        {
            ApplicationUser? user = await this.dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
        }

        public async Task<string> GetFullNameByIdAsync(string userId)
        {
            ApplicationUser? user = await this.dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
        }

        public async Task<IEnumerable<UserViewModel>> AllUsersAsync()
        {
            List<UserViewModel> allUsers = await this.dbContext
                .Users
                .Select(u => new UserViewModel
                {
                    Id = u.Id.ToString(),
                    Email = u.Email,
                    FullName = u.FirstName + " " + u.LastName,
                })
                .ToListAsync();

            foreach (UserViewModel user in allUsers)
            {
                Agent? agent = await this.dbContext
                    .Agents
                    .FirstOrDefaultAsync(a => a.UserId.ToString() == user.Id);

                if (agent != null)
                {
                    user.PhoneNumber = agent.PhoneNumber;
                }
                else
                {
                    user.PhoneNumber = string.Empty;
                }
            }

            return allUsers;
        }
    }
}
