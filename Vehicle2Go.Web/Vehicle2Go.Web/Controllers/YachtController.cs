namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Interfaces;
    using Services.Data.Models.Vehicle;
    using Infrastructure.Extensions;
    using ViewModels.Vehicle;

    using static Common.NotificationMessagesConstants;

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
                this.TempData[ErrorMessage] = "You must become an agent in order to add new yachts";

                return RedirectToAction("Become", "Agent");
            }

            try
            {
                VehicleFormModel formModel = new VehicleFormModel()
                {
                    VehicleCategories = await this.yachtCategoryService.AllCategoriesAsync()
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
                this.TempData[ErrorMessage] = "You must become an agent in order to add new yachts";

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


                string yachtId = await this.yachtService.CreateAndReturnIdAsync(formModel, agentId!);


                return RedirectToAction("Details", "Yacht", new { id = yachtId});
            }
            catch (Exception _)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add new yacht! Please try again later.");

                formModel.VehicleCategories = await this.yachtCategoryService.AllCategoriesAsync();

                return View(formModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<VehicleAllViewModel> myYachts = new List<VehicleAllViewModel>();

            string userId = this.User.GetId()!;

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(userId);
            try
            {
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
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool yachtExist = await this.yachtService.ExistByIdAsync(id);

            if (!yachtExist)
            {
                this.TempData[ErrorMessage] = "Yacht with the provided id does not exist!";

                return RedirectToAction("All", "Yacht");
            }

            try
            {
                VehicleDetailsViewModel viewModel = await this.yachtService
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
            bool carExist = await this.yachtService.ExistByIdAsync(id);

            if (!carExist)
            {
                this.TempData[ErrorMessage] = "Yacht with the provided id does not exist!";

                return RedirectToAction("All", "Yacht");
            }

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit yacht info!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            bool isAgentOwner = await this.yachtService.IsAgentWithIdOwnerOfYachtWithIdAsync(id, agentId!);

            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must be the agent owner of the yacht you want to edit!";

                RedirectToAction("Mine", "Yacht");
            }

            try
            {
                VehicleFormModel formModel = await this.yachtService
                    .GetYachtForEditByIdAsync(id);

                formModel.VehicleCategories = await this.yachtCategoryService.AllCategoriesAsync();

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
                formModel.VehicleCategories = await this.yachtCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            bool carExist = await this.yachtService.ExistByIdAsync(id);

            if (!carExist)
            {
                this.TempData[ErrorMessage] = "Yacht with the provided id does not exist!";

                return RedirectToAction("All", "Yacht");
            }

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit yacht info!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            bool isAgentOwner = await this.yachtService.IsAgentWithIdOwnerOfYachtWithIdAsync(id, agentId!);

            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must be the agent owner of the yacht you want to edit!";

                RedirectToAction("Mine", "Yacht");
            }

            try
            {
                await this.yachtService.EditYachtByIdAndFormModelAsync(id, formModel);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError
                    (string.Empty, "Unexpected error occurred while trying to update the yacht. Please try again later or contact administrator!");

                formModel.VehicleCategories = await this.yachtCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            return RedirectToAction("Details", "Yacht", new { id });
        }

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator!";

            return RedirectToAction("Index", "Home");
        }
    }
}
