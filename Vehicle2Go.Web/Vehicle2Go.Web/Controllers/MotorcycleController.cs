using Microsoft.AspNetCore.Authorization;

namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class MotorcycleController : Controller
    {
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return Ok();
        }
    }
}
