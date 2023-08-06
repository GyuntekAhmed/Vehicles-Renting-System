namespace Vehicle2Go.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Data.Interfaces;
    using ViewModels.Vehicle;
    using Infrastructure.Extensions;

    public class JetController : BaseAdminController
    {
        private readonly IAgentService agentService;
        private readonly IJetService jetService;

        public JetController(IAgentService agentService, IJetService jetService)
        {
            this.agentService = agentService;
            this.jetService = jetService;
        }

        public async Task<IActionResult> Mine()
        {
            string? agentId = await agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

            MyVehiclesViewModel viewModel = new MyVehiclesViewModel()
            {
                AddedVehicles = await jetService.AllByAgentIdAsync(agentId!),
                RentedVehicles = await jetService.AllByUserIdAsync(this.User.GetId()!),
            };

            return View(viewModel);
        }
    }
}
