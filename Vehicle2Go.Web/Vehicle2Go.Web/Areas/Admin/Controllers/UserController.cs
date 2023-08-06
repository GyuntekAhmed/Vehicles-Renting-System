using Vehicle2Go.Web.ViewModels.User;

namespace Vehicle2Go.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Vehicle2Go.Services.Data.Interfaces;

    public class UserController : BaseAdminController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [Route("User/All")]
        public async Task<IActionResult> All()
        {
            IEnumerable<UserViewModel> viewModels =
                await userService.AllUsersAsync();

            return View(viewModels);
        }
    }
}
