namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using ViewModels.Agent;
    using Services.Data.Interfaces;
    using Infrastructure.Extensions;

    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class AgentController : Controller
    {
        private readonly IAgentService agentService;

        public AgentController(IAgentService agentService)
        {
            this.agentService = agentService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = this.User.GetId();

            bool isAgent = await this.agentService.AgentExistByUserIdAsync(userId);

            if (isAgent)
            {
                this.TempData[ErrorMessage] = "You are already an agent!";

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentFormModel model)
        {
            string? userId = this.User.GetId();

            bool isAgent = await this.agentService.AgentExistByUserIdAsync(userId);

            if (isAgent)
            {
                this.TempData[ErrorMessage] = "You are already an agent!";

                return RedirectToAction("Index", "Home");
            }

            bool isPhoneTaken = await this.agentService.AgentExistByPhoneNumberAsync(model.PhoneNumber);

            if (isPhoneTaken)
            {
                this.ModelState.AddModelError
                    (nameof(model.PhoneNumber), "Agent with provided phone number already exist!");
            }

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                await this.agentService.Create(userId, model);
            }
            catch (Exception _)
            {
                this.TempData[ErrorMessage] =
                    "Error occurred while registering you as an agent! Please try again later or contact administrator.";

                return this.RedirectToAction("Index", "Home");
            }

            return this.RedirectToAction("All", "Car");
        }
    }
}
