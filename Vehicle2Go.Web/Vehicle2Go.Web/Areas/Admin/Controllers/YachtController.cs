namespace Vehicle2Go.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Interfaces;
    using ViewModels.Vehicle;
    using Infrastructure.Extensions;

    public class YachtController : BaseAdminController
    {
        private readonly IAgentService agentService;
        private readonly IYachtService yachtService;

        public YachtController(IAgentService agentService, IYachtService yachtService)
        {
            this.agentService = agentService;
            this.yachtService = yachtService;
        }

        public async Task<IActionResult> Mine()
        {
            string? agentId = await agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            MyVehiclesViewModel viewModel = new MyVehiclesViewModel()
            {
                AddedVehicles = await yachtService.AllByAgentIdAsync(agentId!),
                RentedVehicles = await yachtService.AllByUserIdAsync(this.User.GetId()!),
            };

            return View(viewModel);
        }
    }
}
