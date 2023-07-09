namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using ViewModels.Car;
    using Services.Data.Interfaces;
    using static Common.NotificationMessagesConstants;

    public class CarController : BaseController
    {
        private readonly ICarCategoryService carCategoryService;
        private readonly IAgentService agentService;

        public CarController(ICarCategoryService carCategoryService, IAgentService agentService)
        {
            this.carCategoryService = carCategoryService;
            this.agentService = agentService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View();
        }

        [HttpGet]
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
                Categories = await carCategoryService.AllCategoriesAsync()
            };

            return View(model);
        }
    }
}
