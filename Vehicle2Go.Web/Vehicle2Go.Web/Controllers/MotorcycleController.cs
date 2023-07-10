namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using ViewModels.Vehicle;
    using Services.Data.Interfaces;
    using static Common.NotificationMessagesConstants;

    public class MotorcycleController : BaseController
    {
        private readonly IMotorcycleService motorcycleService;
        private readonly IAgentService agentService;

        public MotorcycleController(IMotorcycleService motorcycleService, IAgentService agentService)
        {
            this.motorcycleService = motorcycleService;
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
                Categories = await motorcycleService.AllCategoriesAsync()
            };

            return View(model);
        }
    }
}
