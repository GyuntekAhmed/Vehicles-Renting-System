using Vehicle2Go.Web.Infrastructure.Extensions;

namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Interfaces;
    using ViewModels.Car;

    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class CarController : Controller
    {
        private readonly ICarCategoryService carCategoryService;
        private readonly ICarAgentService carAgentService;

        public CarController(ICarCategoryService carCategoryService, ICarAgentService carAgentService)
        {
            this.carCategoryService = carCategoryService;
            this.carAgentService = carAgentService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isAgent = await this.carAgentService.AgentExistByUserIdAsync(this.User.GetId()!);

            if (!isAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to add new cars";

                return RedirectToAction("Become", "Agent");
            }

            CarFormModel formModel = new CarFormModel()
            {
                CarCategories = await this.carCategoryService.AllCategoriesAsync()
            };

            return View(formModel);
        }
    }
}
