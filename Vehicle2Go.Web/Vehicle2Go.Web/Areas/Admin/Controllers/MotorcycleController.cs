namespace Vehicle2Go.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Interfaces;
    using ViewModels.Vehicle;
    using Infrastructure.Extensions;

    public class MotorcycleController : BaseAdminController
    {
        private readonly IAgentService agentService;
        private readonly IMotorcycleService motorcycleService;

        public MotorcycleController(IMotorcycleService motorcycleService, IAgentService agentService)
        {
            this.motorcycleService = motorcycleService;
            this.agentService = agentService;
        }

        public async Task<IActionResult> Mine()
        {
            string? agentId = await agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            MyVehiclesViewModel viewModel = new MyVehiclesViewModel()
            {
                AddedVehicles = await motorcycleService.AllByAgentIdAsync(agentId!),
                RentedVehicles = await motorcycleService.AllByUserIdAsync(this.User.GetId()!),
            };

            return View(viewModel);
        }
    }
}
