namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using ViewModels.Vehicle;
    using Services.Data.Interfaces;
    using Services.Data.Models.Vehicle;
    using Infrastructure.Extensions;

    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class CarController : Controller
    {
        private readonly ICarCategoryService carCategoryService;
        private readonly IAgentService agentService;
        private readonly ICarService carService;

        public CarController(ICarCategoryService carCategoryService, IAgentService agentService, ICarService carService)
        {
            this.carCategoryService = carCategoryService;
            this.agentService = agentService;
            this.carService = carService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] AllVehiclesQueryModel queryModel)
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

            try
            {
                VehicleFormModel formModel = new VehicleFormModel()
                {
                    VehicleCategories = await this.carCategoryService.AllCategoriesAsync()
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


                string carId = await this.carService.CreateAndReturnIdAsync(formModel, agentId!);

                this.TempData[SuccessMessage] = "Car was added successfully!";

                return RedirectToAction("Details", "Car", new { id = carId });
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add new car! Please try again later.");
                formModel.VehicleCategories = await this.carCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<VehicleAllViewModel> myCars = new List<VehicleAllViewModel>();

            string userId = this.User.GetId()!;

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(userId);
            try
            {
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
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool carExist = await this.carService.ExistByIdAsync(id);

            if (!carExist)
            {
                this.TempData[ErrorMessage] = "Car with the provided id does not exist!";

                return RedirectToAction("All", "Car");
            }

            try
            {
                VehicleDetailsViewModel viewModel = await this.carService
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
            bool carExist = await this.carService.ExistByIdAsync(id);

            if (!carExist)
            {
                this.TempData[ErrorMessage] = "Car with the provided id does not exist!";

                return RedirectToAction("All", "Car");
            }

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit car info!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            bool isAgentOwner = await this.carService.IsAgentWithIdOwnerOfCarWithIdAsync(id, agentId!);

            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must be the agent owner of the car you want to edit!";

                RedirectToAction("Mine", "Car");
            }

            try
            {
                VehicleFormModel formModel = await this.carService
                    .GetCarForEditByIdAsync(id);

                formModel.VehicleCategories = await this.carCategoryService.AllCategoriesAsync();

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
                formModel.VehicleCategories = await this.carCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            bool carExist = await this.carService.ExistByIdAsync(id);

            if (!carExist)
            {
                this.TempData[ErrorMessage] = "Car with the provided id does not exist!";

                return RedirectToAction("All", "Car");
            }

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit car info!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            bool isAgentOwner = await this.carService.IsAgentWithIdOwnerOfCarWithIdAsync(id, agentId!);

            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must be the agent owner of the car you want to edit!";

                RedirectToAction("Mine", "Car");
            }

            try
            {
                await this.carService.EditCarByIdAndFormModelAsync(id, formModel);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError
                    (string.Empty, "Unexpected error occurred while trying to update the car. Please try again later or contact administrator!");

                formModel.VehicleCategories = await this.carCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            this.TempData[SuccessMessage] = "Car was edited successfully!";

            return RedirectToAction("Details", "Car", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool carExist = await carService.ExistByIdAsync(id);

            if (!carExist)
            {
                this.TempData[ErrorMessage] = "Car with the provided id does not exist!";

                return RedirectToAction("All", "Car");
            }

            bool isUserAgent = await agentService.AgentExistByUserIdAsync(User.GetId()!);

            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit car info!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId = await agentService.GetAgentIdByUserIdAsync(User.GetId()!);

            bool isAgentOwner = await carService.IsAgentWithIdOwnerOfCarWithIdAsync(id, agentId!);

            if (!isAgentOwner)
            {
                TempData[ErrorMessage] = "You must be the agent owner of the car you want to edit!";

                RedirectToAction("Mine", "Car");
            }

            try
            {
                VehiclePreDeleteDetailsViewModel viewModel =
                    await carService.GetCarForDeleteByIdAsync(id);

                return View(viewModel);
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, VehiclePreDeleteDetailsViewModel formModel)
        {
            bool carExist = await this.carService.ExistByIdAsync(id);

            if (!carExist)
            {
                this.TempData[ErrorMessage] = "Car with the provided id does not exist!";

                return RedirectToAction("All", "Car");
            }

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to delete car!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            bool isAgentOwner = await this.carService.IsAgentWithIdOwnerOfCarWithIdAsync(id, agentId!);

            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must be the agent owner of the car you want to delete!";

                RedirectToAction("Mine", "Car");
            }

            try
            {
                await this.carService.DeleteByIdAsync(id);

                this.TempData[WarningMessage] = "The car was successfully deleted!";

                return RedirectToAction("Mine", "Car");
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Rent(string id)
        {
            bool carExist = await this.carService.ExistByIdAsync(id);

            if (!carExist)
            {
                this.TempData[ErrorMessage] = "Car with the provided id does not exist!";

                return RedirectToAction("All", "Car");
            }

            bool isCarRented = await this.carService.IsRentedByIdAsync(id);

            if (isCarRented)
            {
                this.TempData[ErrorMessage] =
                    "Selected car is already rented by another user! Please select another car.";

                return RedirectToAction("All", "Car");
            }

            try
            {
                await this.carService.RentCarAsync(id, this.User.GetId()!);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }

            this.TempData[SuccessMessage] = "The car was successfully rented";

            return RedirectToAction("Mine", "Car");
        }

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator!";

            return RedirectToAction("Index", "Home");
        }
    }
}
