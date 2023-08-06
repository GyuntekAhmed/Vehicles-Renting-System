namespace Vehicle2Go.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Interfaces;
    using ViewModels.Vehicle;
    using Infrastructure.Extensions;

    public class TruckController : BaseAdminController
    {
        private readonly IAgentService agentService;
        private readonly ITruckService truckService;

        public TruckController(IAgentService agentService, ITruckService truckService)
        {
            this.agentService = agentService;
            this.truckService = truckService;
        }

        public async Task<IActionResult> Mine()
        {
            string? agentId = await agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            MyVehiclesViewModel viewModel = new MyVehiclesViewModel()
            {
                AddedVehicles = await truckService.AllByAgentIdAsync(agentId!),
                RentedVehicles = await truckService.AllByUserIdAsync(this.User.GetId()!),
            };

            return View(viewModel);
        }
    }
}
