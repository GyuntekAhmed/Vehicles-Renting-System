namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Infrastructure.Extensions;
    using Services.Data.Interfaces;
    using Services.Data.Models.Vehicle;
    using ViewModels.Vehicle;
    
    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class MotorcycleController : Controller
    {
        private readonly IMotorcycleCategoryService motorcycleCategoryService;
        private readonly IAgentService agentService;
        private readonly IMotorcycleService motorcycleService;

        public MotorcycleController(IMotorcycleCategoryService motorcycleCategoryService, IAgentService agentService, IMotorcycleService motorcycleService)
        {
            this.motorcycleCategoryService = motorcycleCategoryService;
            this.agentService = agentService;
            this.motorcycleService = motorcycleService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] AllVehiclesQueryModel queryModel)
        {
            AllVehiclesFilteredAndPagedServiceModel serviceModel =
                await this.motorcycleService.AllAsync(queryModel);

            queryModel.Vehicles = serviceModel.Vehicles;
            queryModel.TotalVehicles = serviceModel.TotalVehiclesCount;
            queryModel.Categories = await this.motorcycleCategoryService.AllCategoryNamesAsync();

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
                VehicleCategories = await this.motorcycleCategoryService.AllCategoriesAsync()
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

            bool categoryExist = await this.motorcycleCategoryService.ExistByIdAsync(formModel.CategoryId);

            if (!categoryExist)
            {
                this.ModelState.AddModelError
                    (nameof(formModel.CategoryId), "Selected category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                formModel.VehicleCategories = await this.motorcycleCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            try
            {
                string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);


                await this.motorcycleService.CreateAsync(formModel, agentId!);
            }
            catch (Exception _)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add new car! Please try again later.");
                formModel.VehicleCategories = await this.motorcycleCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            return RedirectToAction("All", "Motorcycle");
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<VehicleAllViewModel> myMotorcycles = new List<VehicleAllViewModel>();

            string userId = this.User.GetId()!;

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(userId);

            if (isUserAgent)
            {
                string? agentId = await this.agentService.GetAgentIdByUserIdAsync(userId);

                myMotorcycles.AddRange(await this.motorcycleService.AllByAgentIdAsync(agentId!));
            }
            else
            {
                myMotorcycles.AddRange(await this.motorcycleService.AllByUserIdAsync(userId));
            }

            return this.View(myMotorcycles);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool motorcycleExist = await this.motorcycleService.ExistByIdAsync(id);

            if (!motorcycleExist)
            {
                this.TempData[ErrorMessage] = "Motorcycle with the provided id does not exist!";

                return RedirectToAction("All", "Motorcycle");
            }

            VehicleDetailsViewModel? viewModel = await this.motorcycleService
                .GetDetailsByIdAsync(id);
            
            return View(viewModel);
        }
    }
}
