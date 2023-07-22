namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Interfaces;
    using Infrastructure.Extensions;
    using ViewModels.Vehicle;

    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class JetController : Controller
    {
        private readonly IJetCategoryService jetCategoryService;
        private readonly IAgentService agentService;
        private readonly IJetService jetService;

        public JetController(IJetCategoryService jetCategoryService, IAgentService agentService, IJetService jetService)
        {
            this.jetCategoryService = jetCategoryService;
            this.agentService = agentService;
            this.jetService = jetService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to add new cars";

                return RedirectToAction("Become", "Agent");
            }

            VehicleFormModel formModel = new VehicleFormModel()
            {
                VehicleCategories = await this.jetCategoryService.AllCategoriesAsync()
            };

            return View(formModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(VehicleFormModel formModel)
        {
            bool isAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to add new cars";

                return RedirectToAction("Become", "Agent");
            }

            bool categoryExist = await this.jetCategoryService.ExistByIdAsync(formModel.CategoryId);

            if (!categoryExist)
            {
                this.ModelState.AddModelError
                    (nameof(formModel.CategoryId), "Selected category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                formModel.VehicleCategories = await this.jetCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            try
            {
                string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);


                await this.jetService.CreateAsync(formModel, agentId!);
            }
            catch (Exception _)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add new car! Please try again later.");
                formModel.VehicleCategories = await this.jetCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            return RedirectToAction("All", "Car");
        }
    }
}
