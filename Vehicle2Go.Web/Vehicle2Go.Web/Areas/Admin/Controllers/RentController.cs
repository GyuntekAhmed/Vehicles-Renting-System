namespace Vehicle2Go.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    using Services.Data.Interfaces;
    using Web.ViewModels.Rent;

    using static Common.GeneralApplicationConstants;

    public class RentController : BaseAdminController
    {
        private readonly IRentService rentService;
        private readonly IMemoryCache memoryCache;

        public RentController(IRentService rentService,
            IMemoryCache memoryCache)
        {
            this.rentService = rentService;
            this.memoryCache = memoryCache;
        }

        [Route("Rent/All")]
        [ResponseCache(Duration = 120)]
        public async Task<IActionResult> All()
        {
            IEnumerable<RentViewModel> allRents = await this.rentService.AllRentsAsync();
            
            return View(allRents);
        }
    }
}
