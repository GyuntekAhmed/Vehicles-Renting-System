namespace Vehicle2Go.Web.Areas.Admin.ViewModels.Vehicle
{
    using Vehicle2Go.Web.ViewModels.Vehicle;

    public class MyVehiclesViewModel
    {
        public IEnumerable<VehicleAllViewModel> AddedVehicles { get; set; } = null!;

        public IEnumerable<VehicleAllViewModel> RentedVehicles { get; set; } = null!;
    }
}
