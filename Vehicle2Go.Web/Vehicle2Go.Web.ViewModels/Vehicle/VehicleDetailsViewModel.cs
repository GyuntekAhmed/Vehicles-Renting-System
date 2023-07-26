namespace Vehicle2Go.Web.ViewModels.Vehicle
{
    using Agent;

    public class VehicleDetailsViewModel : VehicleAllViewModel
    {
        public string Category { get; set; } = null!;

        public AgentInfoOnVehicleViewModel Agent { get; set; } = null!;
    }
}
