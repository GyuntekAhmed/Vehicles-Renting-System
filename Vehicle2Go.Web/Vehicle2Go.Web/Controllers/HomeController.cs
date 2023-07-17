namespace Vehicle2Go.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    using Services.Data.Interfaces;
    using ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly ICarService vehicleService;

        public HomeController(ICarService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<IndexViewModel> viewModels =
                await vehicleService.AllVehiclesAsync();

            return View(viewModels);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}