namespace Vehicle2Go.Web.ViewModels.Vehicle
{
    using System.ComponentModel.DataAnnotations;

    using Enums;

    using static Common.GeneralApplicationConstants;

    public class AllVehiclesQueryModel
    {
        public AllVehiclesQueryModel()
        {
            this.Categories = new HashSet<string>();
            this.Vehicles = new HashSet<VehicleAllViewModel>();
            this.CurrentPage = DefaultPage;
            this.VehiclesPerPage = EntitiesPerPage;
        }

        public string? Category { get; set; }

        [Display(Name = "Search by word")]
        public string? SearchString { get; set; }

        [Display(Name = "Sort By")]
        public VehicleSorting VehicleSorting { get; set; }

        public int CurrentPage { get; set; }

        [Display(Name = "Show On Page")]
        public int VehiclesPerPage { get; set; }

        public int TotalVehicles { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<VehicleAllViewModel> Vehicles { get; set; }
    }
}
