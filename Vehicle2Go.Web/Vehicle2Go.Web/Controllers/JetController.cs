namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Interfaces;
    using Infrastructure.Extensions;
    using ViewModels.Vehicle;
    using Services.Data.Models.Vehicle;

    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class JetController : Controller
    {
        private readonly IJetCategoryService jetCategoryService;
        private readonly IAgentService agentService;
        private readonly IJetService jetService;

        public JetController(IJetCategoryService jetCategoryService, IAgentService agentService, IJetService jetService)
        {
            this.jetCategoryService = jetCategoryService;
            this.agentService = agentService;
            this.jetService = jetService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] AllVehiclesQueryModel queryModel)
        {
            AllVehiclesFilteredAndPagedServiceModel serviceModel =
                await this.jetService.AllAsync(queryModel);

            queryModel.Vehicles = serviceModel.Vehicles;
            queryModel.TotalVehicles = serviceModel.TotalVehiclesCount;
            queryModel.Categories = await this.jetCategoryService.AllCategoryNamesAsync();

            return this.View(queryModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to add new jets";

                return RedirectToAction("Become", "Agent");
            }

            try
            {
                VehicleFormModel formModel = new VehicleFormModel()
                {
                    VehicleCategories = await this.jetCategoryService.AllCategoriesAsync()
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
                this.TempData[ErrorMessage] = "You must become an agent in order to add new jets";

                return RedirectToAction("Become", "Agent");
            }

            bool categoryExist = await this.jetCategoryService.ExistByIdAsync(formModel.CategoryId);

            if (!categoryExist)
            {
                this.ModelState.AddModelError
                    (nameof(formModel.CategoryId), "Selected category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                formModel.VehicleCategories = await this.jetCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            try
            {
                string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);


                await this.jetService.CreateAsync(formModel, agentId!);
            }
            catch (Exception _)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add new jet! Please try again later.");
                formModel.VehicleCategories = await this.jetCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            return RedirectToAction("Details", "Jet");
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<VehicleAllViewModel> myJets = new List<VehicleAllViewModel>();

            string userId = this.User.GetId()!;

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(userId);
            try
            {
                if (isUserAgent)
                {
                    string? agentId = await this.agentService.GetAgentIdByUserIdAsync(userId);

                    myJets.AddRange(await this.jetService.AllByAgentIdAsync(agentId!));
                }
                else
                {
                    myJets.AddRange(await this.jetService.AllByUserIdAsync(userId));
                }

                return this.View(myJets);
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
            bool jetExist = await this.jetService.ExistByIdAsync(id);

            if (!jetExist)
            {
                this.TempData[ErrorMessage] = "Jet with the provided id does not exist!";

                return RedirectToAction("All", "Jet");
            }

            try
            {
                VehicleDetailsViewModel viewModel = await this.jetService
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
            bool jetExist = await this.jetService.ExistByIdAsync(id);

            if (!jetExist)
            {
                this.TempData[ErrorMessage] = "Jet with the provided id does not exist!";

                return RedirectToAction("All", "Jet");
            }

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit car info!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            bool isAgentOwner = await this.jetService.IsAgentWithIdOwnerOfJetWithIdAsync(id, agentId!);

            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must be the agent owner of the jet you want to edit!";

                RedirectToAction("Mine", "Jet");
            }

            try
            {
                VehicleFormModel formModel = await this.jetService
                    .GetJetForEditByIdAsync(id);

                formModel.VehicleCategories = await this.jetCategoryService.AllCategoriesAsync();

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
                formModel.VehicleCategories = await this.jetCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            bool jetExist = await this.jetService.ExistByIdAsync(id);

            if (!jetExist)
            {
                this.TempData[ErrorMessage] = "Jet with the provided id does not exist!";

                return RedirectToAction("All", "Jet");
            }

            bool isUserAgent = await this.agentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit jet info!";

                return RedirectToAction("Become", "Agent");
            }

            string? agentId = await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            bool isAgentOwner = await this.jetService.IsAgentWithIdOwnerOfJetWithIdAsync(id, agentId!);

            if (!isAgentOwner)
            {
                this.TempData[ErrorMessage] = "You must be the agent owner of the jet you want to edit!";

                RedirectToAction("Mine", "Jet");
            }

            try
            {
                await this.jetService.EditJetByIdAndFormModelAsync(id, formModel);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError
                    (string.Empty, "Unexpected error occurred while trying to update the jet. Please try again later or contact administrator!");

                formModel.VehicleCategories = await this.jetCategoryService.AllCategoriesAsync();

                return View(formModel);
            }

            return RedirectToAction("Details", "Jet", new { id });
        }

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator!";

            return RedirectToAction("Index", "Home");
        }
    }
}
