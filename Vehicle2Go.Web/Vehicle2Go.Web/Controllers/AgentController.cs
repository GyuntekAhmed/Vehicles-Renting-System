namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class AgentController : Controller
    {
        public async Task<IActionResult> Become()
        {
            return View();
        }
    }
}
