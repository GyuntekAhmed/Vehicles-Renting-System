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
                this.TempData[ErrorMessage] = "You must become an agent in order to add new motorcycles";

                return RedirectToAction("Become", "Agent");
            }

            try
            {
                VehicleFormModel formModel = new VehicleFormModel()
                {
                    VehicleCategories = await this.motorcycleCategoryService.AllCategoriesAsync()
                };

                return View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(VehicleFormModel formModel)
        {
            bool isAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to add new motorcycles";

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
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add new motorcycle! Please try again later.");
                formModel.VehicleCategories = await this.motorcycleCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            return RedirectToAction("Details", "Motorcycle");
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<VehicleAllViewModel> myMotorcycles = new List<VehicleAllViewModel>();

            string userId = this.User.GetId()!;

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(userId);
            try
            {
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
            catch (Exception)
            {
                return this.GeneralError();
            }
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

            try
            {
                VehicleDetailsViewModel? viewModel = await this.motorcycleService
                    .GetDetailsByIdAsync(id);

                return View(viewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool motorcycleExist = await this.motorcycleService.ExistByIdAsync(id);

            if (!motorcycleExist)
            {
                this.TempData[ErrorMessage] = "Motorcycle with the provided id does not exist!";

                return RedirectToAction("All", "Motorcycle");
            }

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit motorcycle info!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            bool isAgentOwner = await this.motorcycleService
                .IsAgentWithIdOwnerOfMotorcycleWithIdAsync(id, agentId!);

            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must be the agent owner of the motorcycle you want to edit!";

                RedirectToAction("Mine", "Motorcycle");
            }

            try
            {
                VehicleFormModel formModel = await this.motorcycleService
                    .GetMotorcycleForEditByIdAsync(id);

                formModel.VehicleCategories = await this.motorcycleCategoryService.AllCategoriesAsync();

                return View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, VehicleFormModel formModel)
        {
            if (!this.ModelState.IsValid)
            {
                formModel.VehicleCategories = await this.motorcycleCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            bool carExist = await this.motorcycleService.ExistByIdAsync(id);

            if (!carExist)
            {
                this.TempData[ErrorMessage] = "Motorcycle with the provided id does not exist!";

                return RedirectToAction("All", "Motorcycle");
            }

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit motorcycle info!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            bool isAgentOwner = await this.motorcycleService.IsAgentWithIdOwnerOfMotorcycleWithIdAsync(id, agentId!);

            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must be the agent owner of the motorcycle you want to edit!";

                RedirectToAction("Mine", "Motorcycle");
            }

            try
            {
                await this.motorcycleService.EditMotorcycleByIdAndFormModelAsync(id, formModel);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError
                    (string.Empty, "Unexpected error occurred while trying to update the motorcycle. Please try again later or contact administrator!");

                formModel.VehicleCategories = await this.motorcycleCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            return RedirectToAction("Details", "Motorcycle", new { id });
        }

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator!";

            return RedirectToAction("Index", "Home");
        }
    }
}
