namespace VehiclesRenting.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using ViewModels.Vehicles;
    using Services.Interfaces;
    using static Common.Constants.NotificationMessagesConstants;

    [Authorize]
    public class CarController : BaseController
    {
        private readonly ICategoryService categoriaService;
        private readonly IAgentService agentService;

        public CarController(ICategoryService categoriaService, IAgentService agentService)
        {
            this.categoriaService = categoriaService;
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
            bool isAgent = await agentService.AgentExistByIdAsync(this.GetUserId());

            if (!isAgent)
            {
                TempData[ErrorMessage] = "You need to be an agent to add new cars";

                return RedirectToAction("Become", "Agent");
            }

            AddVehiclesViewModel model = new AddVehiclesViewModel()
            {
                Categories = await categoriaService.AllCategoriesAsync()
            };

            return View(model);
        }
    }
}
