using Vehicle2Go.Web.ViewModels.Agent;

namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Interfaces;
    using static Common.NotificationMessagesConstants;

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
            string? userId = this.GetUserId();

            bool isAgent = await this.agentService.AgentExistByUserIdAsync(userId);

            if (isAgent)
            {
                TempData[ErrorMessage] = "You are already an agent!";

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentFormModel model)
        {
            string? userId = this.GetUserId();

            bool isAgent = await this.agentService.AgentExistByUserIdAsync(userId);

            if (isAgent)
            {
                TempData[ErrorMessage] = "You are already an agent!";

                return RedirectToAction("Index", "Home");
            }

            bool isPhoneNumberUsed = await this.agentService.AgentExistByPhoneNumberAsync(model.PhoneNumber);

            if (isPhoneNumberUsed)
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), "Agent with the provided phone number already exist!");
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
                this.TempData[ErrorMessage] =
                    "Unexpected error occurred while registering you as an agent! Please try again.";

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("All", "Car");
        }
    }
}
