namespace Vehicle2Go.Web.ViewModels.Agent
{
    using System.ComponentModel.DataAnnotations;

    public class AgentInfoOnVehicleViewModel
    {
        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; } = null!;

        public string Address { get; set; } = null!;
    }
}
