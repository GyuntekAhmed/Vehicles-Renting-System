namespace Vehicle2Go.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    using Web.ViewModels.User;
    using Services.Data.Interfaces;

    using static Common.GeneralApplicationConstants;

    public class UserController : BaseAdminController
    {
        private readonly IUserService userService;
        private readonly IMemoryCache memoryCache;

        public UserController(IUserService userService, IMemoryCache memoryCache)
        {
            this.userService = userService;
            this.memoryCache = memoryCache;
        }

        [Route("User/All")]
        public async Task<IActionResult> All()
        {
            IEnumerable<UserViewModel> users = await this.userService.AllUsersAsync();

            return View(users);
        }
    }
}
