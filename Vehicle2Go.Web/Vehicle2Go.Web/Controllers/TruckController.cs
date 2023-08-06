namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Interfaces;
    using Services.Data.Models.Vehicle;
    using Infrastructure.Extensions;
    using ViewModels.Vehicle;

    using static Common.NotificationMessagesConstants;
    using static Common.GeneralApplicationConstants;

    [Authorize]
    public class TruckController : Controller
    {
        private readonly ITruckCategoryService truckCategoryService;
        private readonly IAgentService agentService;
        private readonly ITruckService truckService;
        private readonly IUserService userService;

        public TruckController(ITruckCategoryService truckCategoryService, IAgentService agentService, ITruckService truckService, IUserService userService)
        {
            this.truckCategoryService = truckCategoryService;
            this.agentService = agentService;
            this.truckService = truckService;
            this.userService = userService;
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
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add new truck! Please try again later.");
                formModel.VehicleCategories = await this.truckCategoryService.AllCategoriesAsync();

                return View(formModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            if (this.User.IsInRole(AdminRoleName))
            {
                return RedirectToAction("Mine", "Truck", new { Area = AdminAreaName });
            }

            List<VehicleAllViewModel> myTrucks = new List<VehicleAllViewModel>();

            string userId = this.User.GetId()!;

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(userId);
            try
            {
                if (this.User.IsAdmin())
                {

                    string? agentId = await this.agentService.GetAgentIdByUserIdAsync(userId);

                    myTrucks.AddRange(await this.truckService.AllByAgentIdAsync(agentId!));
                    myTrucks.AddRange(await this.truckService.AllByUserIdAsync(userId));

                    myTrucks = myTrucks.DistinctBy(t => t.Id).ToList();
                }
                else if (isUserAgent)
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
            bool truckExist = await this.truckService.ExistByIdAsync(id);

            if (!truckExist)
            {
                this.TempData[ErrorMessage] = "Truck with the provided id does not exist!";

                return RedirectToAction("All", "Truck");
            }

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isUserAgent && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit truck info!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            bool isAgentOwner = await this.truckService.IsAgentWithIdOwnerOfTruckWithIdAsync(id, agentId!);

            if (!isAgentOwner && !this.User.IsAdmin())
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

            bool truckExist = await this.truckService.ExistByIdAsync(id);

            if (!truckExist)
            {
                this.TempData[ErrorMessage] = "Truck with the provided id does not exist!";

                return RedirectToAction("All", "Truck");
            }

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isUserAgent && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit truck info!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            bool isAgentOwner = await this.truckService.IsAgentWithIdOwnerOfTruckWithIdAsync(id, agentId!);

            if (!isAgentOwner && !this.User.IsAdmin())
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

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool truckExist = await this.truckService.ExistByIdAsync(id);

            if (!truckExist)
            {
                this.TempData[ErrorMessage] = "Truck with the provided id does not exist!";

                return RedirectToAction("All", "Truck");
            }

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isUserAgent && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit truck info!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            bool isAgentOwner = await this.truckService.IsAgentWithIdOwnerOfTruckWithIdAsync(id, agentId!);

            if (!isAgentOwner && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must be the agent owner of the truck you want to edit!";

                RedirectToAction("Mine", "Truck");
            }

            try
            {
                VehiclePreDeleteDetailsViewModel viewModel =
                    await this.truckService.GetTruckForDeleteByIdAsync(id);

                return View(viewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string truckId, VehiclePreDeleteDetailsViewModel viewModel)
        {
            bool truckExist = await this.truckService.ExistByIdAsync(truckId);

            if (!truckExist)
            {
                this.TempData[ErrorMessage] = "Truck with the provided id does not exist!";

                return RedirectToAction("All", "Truck");
            }

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isUserAgent && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to delete truck!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            bool isAgentOwner = await this.truckService
                .IsAgentWithIdOwnerOfTruckWithIdAsync(truckId, agentId!);

            if (!isAgentOwner && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must be the agent owner of the truck you want to delete!";

                RedirectToAction("Mine", "Truck");
            }

            try
            {
                await this.truckService.DeleteByIdAsync(truckId);

                this.TempData[WarningMessage] = "The truck was successfully deleted!";

                return RedirectToAction("Mine", "Truck");
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Rent(string id)
        {
            bool truckExist = await this.truckService.ExistByIdAsync(id);

            if (!truckExist)
            {
                this.TempData[ErrorMessage] = "Truck with the provided id does not exist!";

                return RedirectToAction("All", "Truck");
            }

            bool isTruckRented = await this.truckService.IsRentedByIdAsync(id);

            if (isTruckRented)
            {
                this.TempData[ErrorMessage] =
                    "Selected truck is already rented by another user! Please select another truck.";

                return RedirectToAction("All", "Truck");
            }

            try
            {
                await this.truckService.RentTruckAsync(id, this.User.GetId()!);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }

            this.TempData[SuccessMessage] = "The truck was successfully rented";

            return RedirectToAction("Mine", "Truck");
        }

        [HttpPost]
        public async Task<IActionResult> Leave(string id)
        {
            bool truckExist = await this.truckService.ExistByIdAsync(id);

            if (!truckExist)
            {
                this.TempData[ErrorMessage] = "Truck with the provided id does not exist!";

                return RedirectToAction("All", "Truck");
            }

            bool isTruckRented = await this.truckService.IsRentedByIdAsync(id);

            if (!isTruckRented)
            {
                this.TempData[ErrorMessage] = "Selected truck is not rented!"; ;

                return RedirectToAction("All", "Truck");
            }

            bool isUserRenter = await this.truckService.IsRentedByUserWithIdAsync(id, this.User.GetId()!);

            if (!isUserRenter)
            {
                this.TempData[ErrorMessage] = "You must be the renter of the truck to leave it!";

                return RedirectToAction("Mine", "Truck");
            }

            try
            {
                await this.truckService.LeaveAsync(id);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }

            this.TempData[InformationMessage] = "The truck was successfully leave";

            return RedirectToAction("Mine", "Truck");
        }

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator!";

            return RedirectToAction("Index", "Home");
        }
    }
}
