
namespace VehiclesRenting.Web.ViewModels.Agent
{
    using System.ComponentModel.DataAnnotations;

    using static Common.Constants.EntityValidationConstants.Agent;

    public class BecomeAgentFormModel
    {
        [Required]
        [Phone]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;
    }
}
