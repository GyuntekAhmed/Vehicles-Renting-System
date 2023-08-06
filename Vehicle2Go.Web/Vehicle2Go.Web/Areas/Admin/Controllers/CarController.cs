namespace Vehicle2Go.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using ViewModels.Vehicle;
    using Services.Data.Interfaces;
    using Infrastructure.Extensions;

    public class CarController : BaseAdminController
    {
        private readonly IAgentService agentService;
        private readonly ICarService carService;

        public CarController(IAgentService agentService, ICarService carService)
        {
            this.agentService = agentService;
            this.carService = carService;
        }

        public async Task<IActionResult> Mine()
        {
            string? agentId = await agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            MyVehiclesViewModel viewModel = new MyVehiclesViewModel()
            {
                AddedVehicles = await carService.AllByAgentIdAsync(agentId!),
                RentedVehicles = await carService.AllByUserIdAsync(this.User.GetId()!),
            };

            return View(viewModel);
        }
    }
}
