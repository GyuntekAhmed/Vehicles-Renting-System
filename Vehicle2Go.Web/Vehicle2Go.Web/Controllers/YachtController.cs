namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
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
                Categories = await yachtService.AllCategoriesAsync()
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
            bool categoryExists = await yachtService.CategoryExistByIdAsync(model.CategoryId);

            if (!categoryExists)
            {
                ModelState.AddModelError
                    (nameof(model.CategoryId), "Selected category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await yachtService.AllCategoriesAsync();

                return View(model);
            }

            try
            {
                string? agentId = await agentService.GetAgentIdAsync(this.GetUserId());

                await yachtService.CreateAsync(model, agentId!);
            }
            catch (Exception _)
            {
                ModelState.AddModelError
                    (string.Empty, "Unexpected error while trying to add new yacht! Please try again later!");
                model.Categories = await yachtService.AllCategoriesAsync();

                return View(model);
            }

            return RedirectToAction("All", "Yacht");
        }
    }
}
