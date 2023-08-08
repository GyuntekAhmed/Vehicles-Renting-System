namespace Vehicle2Go.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Interfaces;
    using Web.ViewModels.Rent;

    public class RentController : BaseAdminController
    {
        private readonly IRentService rentService;

        public RentController(IRentService rentService)
        {
            this.rentService = rentService;
        }

        [Route("Rent/All")]
        public async Task<IActionResult> All()
        {
            IEnumerable<RentViewModel> allRents = await rentService.AllRentsAsync();

            return View(allRents);
        }
    }
}
