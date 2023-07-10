namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Vehicle2Go.Services.Data.Interfaces;
    using ViewModels.Vehicle;
    using static Common.NotificationMessagesConstants;

    public class YachtController : BaseController
    {
        private readonly IYachtService yachtService;
        private readonly IAgentService agentService;

        public YachtController(IYachtService yachtService, IAgentService agentService)
        {
            this.yachtService = yachtService;
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
                Categories = await yachtService.AllCategoriesAsync()
            };

            return View(model);
        }
    }
}
