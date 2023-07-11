namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using ViewModels.Vehicle;
    using Services.Data.Interfaces;
    using static Common.NotificationMessagesConstants;
    using Microsoft.AspNetCore.Authorization;
    using Vehicle2Go.Services.Data;

    public class MotorcycleController : BaseController
    {
        private readonly IMotorcycleService motorcycleService;
        private readonly IAgentService agentService;

        public MotorcycleController(IMotorcycleService motorcycleService, IAgentService agentService)
        {
            this.motorcycleService = motorcycleService;
            this.agentService = agentService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return Ok();
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

        [HttpPost]
        public async Task<IActionResult> Add(AddVehicleViewModel model)
        {
            bool isAgent = await agentService.AgentExistByUserIdAsync(this.GetUserId());

            if (!isAgent)
            {
                this.TempData[ErrorMessage] = "You need become an agent to add new cars!";

                return RedirectToAction("Become", "Agent");
            }

            bool categoryExists = await motorcycleService.CategoryExistByIdAsync(model.CategoryId);

            if (!categoryExists)
            {
                ModelState.AddModelError
                    (nameof(model.CategoryId), "Selected category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await motorcycleService.AllCategoriesAsync();

                return View(model);
            }

            try
            {
                string? agentId = await agentService.GetAgentIdAsync(this.GetUserId());

                await motorcycleService.CreateAsync(model, agentId!);
            }
            catch (Exception _)
            {
                ModelState.AddModelError
                    (string.Empty, "Unexpected error while trying to add new motorcycle! Please try again later!");
                model.Categories = await motorcycleService.AllCategoriesAsync();

                return View(model);
            }

            return RedirectToAction("All", "Motorcycle");
        }
    }
}
