namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Interfaces;
    using ViewModels.Vehicle;
    using static Common.NotificationMessagesConstants;

    public class JetController : BaseController
    {
        private readonly IJetService jetService;
        private readonly IAgentService agentService;

        public JetController(IJetService jetService, IAgentService agentService)
        {
            this.jetService = jetService;
            this.agentService = agentService;
        }

        public async Task<IActionResult> Add()
        {
            bool isAgent = await agentService.AgentExistByUserIdAsync(this.GetUserId());

            if (!isAgent)
            {
                this.TempData[ErrorMessage] = "You need become an agent to add new cars!";

                return RedirectToAction("Become", "Agent");
            }

            AddVehicleViewModel model = new AddVehicleViewModel()
            {
                Categories = await jetService.AllCategoriesAsync()
            };

            return View(model);
        }
    }
}
