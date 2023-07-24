namespace Vehicle2Go.Services.Data.Models.Vehicle
{
    using Vehicle2Go.Web.ViewModels.Vehicle;

    public class AllVehiclesFilteredAndPagedServiceModel
    {
        public AllVehiclesFilteredAndPagedServiceModel()
        {
            this.Vehicles = new HashSet<VehicleAllViewModel>();
        }

        public int TotalVehiclesCount { get; set; }

        public IEnumerable<VehicleAllViewModel> Vehicles { get; set; }
    }
}
