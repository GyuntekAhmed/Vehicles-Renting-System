namespace VehiclesRenting.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Services.Interfaces;
    using ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IVehicleService vehicleService;

        public HomeController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<IndexViewModel> vehicleViewModels = await this.vehicleService.AllVehiclesAsync();

            return View(vehicleViewModels);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}