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
        private readonly IUserService userService;

        public YachtController(IYachtCategoryService yachtCategoryService, IAgentService agentService, IYachtService yachtService, IUserService userService)
        {
            this.yachtCategoryService = yachtCategoryService;
            this.agentService = agentService;
            this.yachtService = yachtService;
            this.userService = userService;
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

                this.TempData[SuccessMessage] = "Yacht was added successfully!";

                return RedirectToAction("Details", "Yacht", new { id = yachtId});
            }
            catch (Exception)
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
            bool yachtExist = await this.yachtService.ExistByIdAsync(id);

            if (!yachtExist)
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

            bool yachtExist = await this.yachtService.ExistByIdAsync(id);

            if (!yachtExist)
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

            this.TempData[SuccessMessage] = "Yacht was edited successfully!";

            return RedirectToAction("Details", "Yacht", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool yachtExist = await this.yachtService.ExistByIdAsync(id);

            if (!yachtExist)
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
                VehiclePreDeleteDetailsViewModel viewModel =
                    await this.yachtService.GetYachtForDeleteByIdAsync(id);

                return View(viewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string yachtId, VehiclePreDeleteDetailsViewModel viewModel)
        {
            bool yachtExist = await this.yachtService.ExistByIdAsync(yachtId);

            if (!yachtExist)
            {
                this.TempData[ErrorMessage] = "Yacht with the provided id does not exist!";

                return RedirectToAction("All", "Yacht");
            }

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to delete yacht!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            bool isAgentOwner = await this.yachtService
                .IsAgentWithIdOwnerOfYachtWithIdAsync(yachtId, agentId!);

            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must be the agent owner of the yacht you want to delete!";

                RedirectToAction("Mine", "Yacht");
            }

            try
            {
                await this.yachtService.DeleteByIdAsync(yachtId);

                this.TempData[WarningMessage] = "The yacht was successfully deleted!";

                return RedirectToAction("Mine", "Yacht");
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Rent(string id)
        {
            bool yachtExist = await this.yachtService.ExistByIdAsync(id);

            if (!yachtExist)
            {
                this.TempData[ErrorMessage] = "Yacht with the provided id does not exist!";

                return RedirectToAction("All", "Yacht");
            }

            bool isYachtRented = await this.yachtService.IsRentedByIdAsync(id);

            if (isYachtRented)
            {
                this.TempData[ErrorMessage] =
                    "Selected yacht is already rented by another user! Please select another yacht.";

                return RedirectToAction("All", "Yacht");
            }

            try
            {
                await this.yachtService.RentYachtAsync(id, this.User.GetId()!);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }

            this.TempData[SuccessMessage] = "The yacht was successfully rented";

            return RedirectToAction("Mine", "Yacht");
        }

        [HttpPost]
        public async Task<IActionResult> Leave(string id)
        {
            bool yachtExist = await this.yachtService.ExistByIdAsync(id);

            if (!yachtExist)
            {
                this.TempData[ErrorMessage] = "Yacht with the provided id does not exist!";

                return RedirectToAction("All", "Yacht");
            }

            bool isYachtRented = await this.yachtService.IsRentedByIdAsync(id);

            if (!isYachtRented)
            {
                this.TempData[ErrorMessage] = "Selected yacht is not rented!"; ;

                return RedirectToAction("All", "Yacht");
            }

            bool isUserRenter = await this.yachtService.IsRentedByUserWithIdAsync(id, this.User.GetId()!);

            if (!isUserRenter)
            {
                this.TempData[ErrorMessage] = "You must be the renter of the yacht to leave it!";

                return RedirectToAction("Mine", "Yacht");
            }

            try
            {
                await this.yachtService.LeaveAsync(id);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }

            this.TempData[InformationMessage] = "The yacht was successfully leave";

            return RedirectToAction("Mine", "Yacht");
        }

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator!";

            return RedirectToAction("Index", "Home");
        }
    }
}
