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
    public class TruckController : Controller
    {
        private readonly ITruckCategoryService truckCategoryService;
        private readonly IAgentService agentService;
        private readonly ITruckService truckService;

        public TruckController(ITruckCategoryService truckCategoryService, IAgentService agentService, ITruckService truckService)
        {
            this.truckCategoryService = truckCategoryService;
            this.agentService = agentService;
            this.truckService = truckService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] AllVehiclesQueryModel queryModel)
        {
            AllVehiclesFilteredAndPagedServiceModel serviceModel =
                await this.truckService.AllAsync(queryModel);

            queryModel.Vehicles = serviceModel.Vehicles;
            queryModel.TotalVehicles = serviceModel.TotalVehiclesCount;
            queryModel.Categories = await this.truckCategoryService.AllCategoryNamesAsync();

            return this.View(queryModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to add new trucks";

                return RedirectToAction("Become", "Agent");
            }

            try
            {
                VehicleFormModel formModel = new VehicleFormModel()
                {
                    VehicleCategories = await this.truckCategoryService.AllCategoriesAsync()
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
                this.TempData[ErrorMessage] = "You must become an agent in order to add new trucks";

                return RedirectToAction("Become", "Agent");
            }

            bool categoryExist = await this.truckCategoryService.ExistByIdAsync(formModel.CategoryId);

            if (!categoryExist)
            {
                this.ModelState.AddModelError
                    (nameof(formModel.CategoryId), "Selected category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                formModel.VehicleCategories = await this.truckCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            try
            {
                string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);


                string truckId = await this.truckService.CreateAndReturnIdAsync(formModel, agentId!);

                this.TempData[SuccessMessage] = "Truck was added successfully!";


                return RedirectToAction("Details", "Truck", new { id = truckId});
            }
            catch (Exception _)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add new truck! Please try again later.");
                formModel.VehicleCategories = await this.truckCategoryService.AllCategoriesAsync();

                return View(formModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<VehicleAllViewModel> myTrucks = new List<VehicleAllViewModel>();

            string userId = this.User.GetId()!;

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(userId);
            try
            {
                if (isUserAgent)
                {
                    string? agentId = await this.agentService.GetAgentIdByUserIdAsync(userId);

                    myTrucks.AddRange(await this.truckService.AllByAgentIdAsync(agentId!));
                }
                else
                {
                    myTrucks.AddRange(await this.truckService.AllByUserIdAsync(userId));
                }

                return this.View(myTrucks);
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
            bool truckExist = await this.truckService.ExistByIdAsync(id);

            if (!truckExist)
            {
                this.TempData[ErrorMessage] = "Truck with the provided id does not exist!";

                return RedirectToAction("All", "Truck");
            }

            try
            {
                VehicleDetailsViewModel? viewModel = await this.truckService
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
            bool carExist = await this.truckService.ExistByIdAsync(id);

            if (!carExist)
            {
                this.TempData[ErrorMessage] = "Truck with the provided id does not exist!";

                return RedirectToAction("All", "Truck");
            }

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit truck info!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            bool isAgentOwner = await this.truckService.IsAgentWithIdOwnerOfTruckWithIdAsync(id, agentId!);

            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must be the agent owner of the truck you want to edit!";

                RedirectToAction("Mine", "Truck");
            }

            try
            {
                VehicleFormModel formModel = await this.truckService
                    .GetTruckForEditByIdAsync(id);

                formModel.VehicleCategories = await this.truckCategoryService.AllCategoriesAsync();

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
                formModel.VehicleCategories = await this.truckCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            bool carExist = await this.truckService.ExistByIdAsync(id);

            if (!carExist)
            {
                this.TempData[ErrorMessage] = "Truck with the provided id does not exist!";

                return RedirectToAction("All", "Truck");
            }

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit truck info!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            bool isAgentOwner = await this.truckService.IsAgentWithIdOwnerOfTruckWithIdAsync(id, agentId!);

            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must be the agent owner of the truck you want to edit!";

                RedirectToAction("Mine", "Truck");
            }

            try
            {
                await this.truckService.EditTruckByIdAndFormModelAsync(id, formModel);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError
                    (string.Empty, "Unexpected error occurred while trying to update the truck. Please try again later or contact administrator!");

                formModel.VehicleCategories = await this.truckCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            this.TempData[SuccessMessage] = "Truck was edited successfully!";

            return RedirectToAction("Details", "Truck", new { id });
        }

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator!";

            return RedirectToAction("Index", "Home");
        }
    }
}
