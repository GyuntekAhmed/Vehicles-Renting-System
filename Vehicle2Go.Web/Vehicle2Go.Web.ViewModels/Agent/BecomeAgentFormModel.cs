namespace Vehicle2Go.Web.ViewModels.Agent
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.AgentConstants;

    public class BecomeAgentFormModel
    {
        [Required]
        [Phone]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; } = null!;
    }
}
