using Vehicle2Go.Services.Data.Interfaces;
using Vehicle2Go.Web.Infrastructure.Extensions;

namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CarAgentController : Controller
    {
        private readonly ICarAgentService carAgentService;

        public CarAgentController(ICarAgentService carAgentService)
        {
            this.carAgentService = carAgentService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = this.User.GetId();

            bool isAgent = await carAgentService.AgentExistByUserIdAsync(userId);

            if (isAgent)
            {
                return this.BadRequest();
            }

            return View();
        }
    }
}
