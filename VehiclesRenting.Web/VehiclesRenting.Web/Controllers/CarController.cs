using VehiclesRenting.Services.Interfaces;

namespace VehiclesRenting.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CarController : BaseController
    {
        private readonly ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var model = await carService.AllCarsAsync();

            return View(model);
        }
    }
}
