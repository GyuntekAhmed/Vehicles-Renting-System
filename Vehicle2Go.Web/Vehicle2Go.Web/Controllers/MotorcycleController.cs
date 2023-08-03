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
        private readonly IUserService userService;

        public MotorcycleController(IMotorcycleCategoryService motorcycleCategoryService, IAgentService agentService, IMotorcycleService motorcycleService, IUserService userService)
        {
            this.motorcycleCategoryService = motorcycleCategoryService;
            this.agentService = agentService;
            this.motorcycleService = motorcycleService;
            this.userService = userService;
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


                string motorcycleId = await this.motorcycleService.CreateAndReturnIdAsync(formModel, agentId!);

                this.TempData[SuccessMessage] = "Motorcycle was added successfully!";
                
                return RedirectToAction("Details", "Motorcycle", new { id = motorcycleId});
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add new motorcycle! Please try again later.");
                formModel.VehicleCategories = await this.motorcycleCategoryService.AllCategoriesAsync();

                return View(formModel);
            }
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

                viewModel.Agent.FullName = await this.userService.GetFullNameAsync(this.User.Identity?.Name!);

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

            this.TempData[SuccessMessage] = "Motorcycle was edited successfully!";

            return RedirectToAction("Details", "Motorcycle", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
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
                VehiclePreDeleteDetailsViewModel viewModel =
                    await this.motorcycleService.GetMotorcycleForDeleteByIdAsync(id);

                return View(viewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string motorcycleId, VehiclePreDeleteDetailsViewModel viewModel)
        {
            bool motorcycleExist = await this.motorcycleService.ExistByIdAsync(motorcycleId);

            if (!motorcycleExist)
            {
                this.TempData[ErrorMessage] = "Motorcycle with the provided id does not exist!";

                return RedirectToAction("All", "Motorcycle");
            }

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to delete motorcycle!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            bool isAgentOwner = await this.motorcycleService
                .IsAgentWithIdOwnerOfMotorcycleWithIdAsync(motorcycleId, agentId!);

            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] =
                    "You must be the agent owner of the motorcycle you want to delete!";

                RedirectToAction("Mine", "Motorcycle");
            }

            try
            {
                await this.motorcycleService.DeleteByIdAsync(motorcycleId);

                this.TempData[WarningMessage] = "The motorcycle was successfully deleted!";

                return RedirectToAction("Mine", "Motorcycle");
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Rent(string id)
        {
            bool motorcycleExist = await this.motorcycleService.ExistByIdAsync(id);

            if (!motorcycleExist)
            {
                this.TempData[ErrorMessage] = "Motorcycle with the provided id does not exist!";

                return RedirectToAction("All", "Motorcycle");
            }

            bool isMotorcycleRented = await this.motorcycleService.IsRentedByIdAsync(id);

            if (isMotorcycleRented)
            {
                this.TempData[ErrorMessage] =
                    "Selected motorcycle is already rented by another user! Please select another motorcycle.";

                return RedirectToAction("All", "Motorcycle");
            }

            try
            {
                await this.motorcycleService.RentMotorcycleAsync(id, this.User.GetId()!);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }

            this.TempData[SuccessMessage] = "The motorcycle was successfully rented";

            return RedirectToAction("Mine", "Motorcycle");
        }

        [HttpPost]
        public async Task<IActionResult> Leave(string id)
        {
            bool motorcycleExist = await this.motorcycleService.ExistByIdAsync(id);

            if (!motorcycleExist)
            {
                this.TempData[ErrorMessage] = "Motorcycle with the provided id does not exist!";

                return RedirectToAction("All", "Motorcycle");
            }

            bool isMotorcycleRented = await this.motorcycleService.IsRentedByIdAsync(id);

            if (!isMotorcycleRented)
            {
                this.TempData[ErrorMessage] = "Selected motorcycle is not rented!"; ;

                return RedirectToAction("All", "Motorcycle");
            }

            bool isUserRenter =
                await this.motorcycleService.IsRentedByUserWithIdAsync(id, this.User.GetId()!);

            if (!isUserRenter)
            {
                this.TempData[ErrorMessage] = "You must be the renter of the motorcycle to leave it!";

                return RedirectToAction("Mine", "Motorcycle");
            }

            try
            {
                await this.motorcycleService.LeaveAsync(id);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }

            this.TempData[InformationMessage] = "The motorcycle was successfully leave";

            return RedirectToAction("Mine", "Motorcycle");
        }

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator!";

            return RedirectToAction("Index", "Home");
        }
    }
}
