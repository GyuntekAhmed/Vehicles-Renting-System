using VehiclesRenting.Web.ViewModels.Agent;

namespace VehiclesRenting.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Interfaces;
    using static Common.Constants.NotificationMessagesConstants;

    [Authorize]
    public class AgentController : BaseController
    {
        private readonly IAgentService agentService;

        public AgentController(IAgentService agentService)
        {
            this.agentService = agentService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string userId = GetUserId();

            bool isAgent = await this.agentService.AgentExistByIdAsync(userId);

            if (isAgent)
            {
                TempData[ErrorMessage] = "You are already an agent";

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentFormModel model)
        {
            string userId = GetUserId();

            bool isAgent = await this.agentService.AgentExistByIdAsync(userId);

            if (isAgent)
            {
                TempData[ErrorMessage] = "You are already an agent";

                return RedirectToAction("Index", "Home");
            }

            bool isPhoneUsed = await agentService.AgentExistsByPhoneAsync(model.PhoneNumber);

            if (isPhoneUsed)
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), "Agent with this phone number is already used");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {

                await this.agentService.Create(userId, model);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error! Please try again later or contact with administrator!";

                return RedirectToAction("Index", "Home");
            }

            return this.RedirectToAction("All", "Car");
        }
    }
}
