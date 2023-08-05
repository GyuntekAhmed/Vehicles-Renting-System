namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    using Services.Data.Interfaces;
    using ViewModels.Home;

    using static Common.GeneralApplicationConstants;

    public class HomeController : Controller
    {
        private readonly IVehicleService vehicleService;

        public HomeController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (this.User.IsInRole(AdminRoleName))
            {
                return RedirectToAction("Index", "Home", new { Area = AdminAreaName });
            }

            IEnumerable<IndexViewModel> viewModels =
                await vehicleService.AllVehiclesAsync();

            return View(viewModels);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400 || statusCode == 404)
            {
                return View("Error404");
            }

            if (statusCode == 401)
            {
                return View("Error401");
            }

            return View();
        }
    }
}