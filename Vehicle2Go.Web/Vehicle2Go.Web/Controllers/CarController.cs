using Vehicle2Go.Services.Data.Models.Vehicle;

namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using ViewModels.Vehicle;
    using Services.Data.Interfaces;
    using Infrastructure.Extensions;

    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class CarController : Controller
    {
        private readonly ICarCategoryService carCategoryService;
        private readonly IAgentService agentService;
        private ICarService carService;

        public CarController(ICarCategoryService carCategoryService, IAgentService agentService, ICarService carService)
        {
            this.carCategoryService = carCategoryService;
            this.agentService = agentService;
            this.carService = carService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery]AllVehiclesQueryModel queryModel)
        {
            AllVehiclesFilteredAndPagedServiceModel serviceModel =
                await this.carService.AllAsync(queryModel);

            queryModel.Vehicles = serviceModel.Vehicles;
            queryModel.TotalVehicles = serviceModel.TotalVehiclesCount;
            queryModel.Categories = await this.carCategoryService.AllCategoryNamesAsync();

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
                VehicleCategories = await this.carCategoryService.AllCategoriesAsync()
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

            bool categoryExist = await this.carCategoryService.ExistByIdAsync(formModel.CategoryId);

            if (!categoryExist)
            {
                this.ModelState.AddModelError
                    (nameof(formModel.CategoryId), "Selected category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                formModel.VehicleCategories = await this.carCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            try
            {
                string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);


                await this.carService.CreateAsync(formModel, agentId!);
            }
            catch (Exception _)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add new car! Please try again later.");
                formModel.VehicleCategories = await this.carCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            return RedirectToAction("All", "Car");
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<VehicleAllViewModel> myCars = new List<VehicleAllViewModel>();

            string userId = this.User.GetId()!;

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(userId);

            if (isUserAgent)
            {
                string? agentId = await this.agentService.GetAgentIdByUserIdAsync(userId);

                myCars.AddRange(await this.carService.AllByAgentIdAsync(agentId!));
            }
            else
            {
                myCars.AddRange(await this.carService.AllByUserIdAsync(userId));
            }

            return this.View(myCars);
        }
    }
}
