using VehiclesRenting.Services.Interfaces;

namespace VehiclesRenting.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Diagnostics;

    using ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly ICarService carService;
        private readonly IMotorcycleService motorcycleService;
        private readonly IScooterService scooterService;

        public HomeController(ICarService carService, IMotorcycleService motorcycleService, IScooterService scooterService)
        {
            this.carService = carService;
            this.motorcycleService = motorcycleService;
            this.scooterService = scooterService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<IndexViewModel> carViewModel = await carService.AllCarsAsync();
            IEnumerable<IndexViewModel> motorViewModel = await motorcycleService.AllMotorcyclesAsync();
            IEnumerable<IndexViewModel> scooterViewModel = await scooterService.AllScootersAsync();

            var allModels = new List<object>
            {
                carViewModel,
                motorViewModel,
                scooterViewModel,
            };

            return View(allModels);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}