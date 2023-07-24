namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Interfaces;
    using Infrastructure.Extensions;
    using ViewModels.Vehicle;

    using static Common.NotificationMessagesConstants;
    using Vehicle2Go.Services.Data.Models.Vehicle;

    [Authorize]
    public class YachtController : Controller
    {
        private readonly IYachtCategoryService yachtCategoryService;
        private readonly IAgentService agentService;
        private readonly IYachtService yachtService;

        public YachtController(IYachtCategoryService yachtCategoryService, IAgentService agentService, IYachtService yachtService)
        {
            this.yachtCategoryService = yachtCategoryService;
            this.agentService = agentService;
            this.yachtService = yachtService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] AllVehiclesQueryModel queryModel)
        {
            AllVehiclesFilteredAndPagedServiceModel serviceModel =
                await this.yachtService.AllAsync(queryModel);

            queryModel.Vehicles = serviceModel.Vehicles;
            queryModel.TotalVehicles = serviceModel.TotalVehiclesCount;
            queryModel.Categories = await this.yachtCategoryService.AllCategoryNamesAsync();

            return this.View(queryModel);
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
                VehicleCategories = await this.yachtCategoryService.AllCategoriesAsync()
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

            bool categoryExist = await this.yachtCategoryService.ExistByIdAsync(formModel.CategoryId);

            if (!categoryExist)
            {
                this.ModelState.AddModelError
                    (nameof(formModel.CategoryId), "Selected category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                formModel.VehicleCategories = await this.yachtCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            try
            {
                string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);


                await this.yachtService.CreateAsync(formModel, agentId!);
            }
            catch (Exception _)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add new car! Please try again later.");
                formModel.VehicleCategories = await this.yachtCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            return RedirectToAction("All", "Car");
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<VehicleAllViewModel> myYachts = new List<VehicleAllViewModel>();

            string userId = this.User.GetId()!;
                
            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(userId);

            if (isUserAgent)
            {
                string? agentId = await this.agentService.GetAgentIdByUserIdAsync(userId);

                myYachts.AddRange(await this.yachtService.AllByAgentIdAsync(agentId!));
            }
            else
            {
                myYachts.AddRange(await this.yachtService.AllByUserIdAsync(userId));
            }

            return this.View(myYachts);
        }
    }
}
